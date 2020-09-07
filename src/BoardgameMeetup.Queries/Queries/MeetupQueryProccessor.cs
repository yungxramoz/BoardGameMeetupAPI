using BoardgameMeetup.Api.Common.Exceptions;
using BoardgameMeetup.Api.Models.Meetup;
using BoardgameMeetup.Data.Access.DAL;
using BoardgameMeetup.Data.Models;
using BoardgameMeetup.Security;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoardgameMeetup.Queries.Queries
{
    public class MeetupQueryProccessor : IMeetupQueryProcessor
    {
        private readonly IUnitOfWork _unitOfOfWork;
        private readonly ISecurityContext _securityContext;

        public MeetupQueryProccessor(IUnitOfWork unitOfOfWork, ISecurityContext securityContext)
        {
            _unitOfOfWork = unitOfOfWork;
            _securityContext = securityContext;
        }

        public async Task<Meetup> Create(CreateMeetupModel model)
        {
            var meetup = new Meetup
            {
                UserId = _securityContext.User.Id,
                Title = model.Title,
                Description = model.Description,
                Date = model.Date,
                ParticipantCount = model.ParticipantCount,
                Place = model.Place,
                Plz = model.Plz
            };

            _unitOfOfWork.Add(meetup);
            await _unitOfOfWork.CommitAsync();

            return meetup;
        }

        public async Task Delete(Guid id)
        {
            var meetup = GetQuery().FirstOrDefault(x => x.Id == id);

            if(meetup == null)
            {
                throw new NotFoundException("Meetup [" + id + "] not found");
            }

            if (meetup.IsCancled)
            {
                return;
            }

            meetup.IsCancled = true;
            await _unitOfOfWork.CommitAsync();
        }

        public IQueryable<Meetup> Get()
        {
            var query = GetQuery();
            return query;
        }

        public Meetup Get(Guid id)
        {
            var meetup = GetQuery().FirstOrDefault(x => x.Id == id);

            if(meetup == null)
            {
                throw new NotFoundException("Meetup[" + id + "] not found");
            }

            return meetup;
        }

        public async Task<Meetup> Update(Guid id, UpdateMeetupModel model)
        {
            var meetup = GetQuery().FirstOrDefault(x => x.Id == id);

            if (meetup == null)
            {
                throw new NotFoundException("Meetup[" + id + "] not found");
            }

            meetup.Title = model.Title;
            meetup.Description = model.Description;
            meetup.ParticipantCount = model.ParticipantCount;
            meetup.Date = model.Date;
            meetup.Place = model.Place;
            meetup.Plz = model.Plz;

            await _unitOfOfWork.CommitAsync();
            return meetup;
        }

        private IQueryable<Meetup> GetQuery()
        {
            var q = _unitOfOfWork.Query<Meetup>()
                .Where(x => !x.IsCancled && x.Date > DateTime.UtcNow);

            if (!_securityContext.IsAdministrator)
            {
                //handle not admin specific data access
            }

            return q;
        }
    }
}
