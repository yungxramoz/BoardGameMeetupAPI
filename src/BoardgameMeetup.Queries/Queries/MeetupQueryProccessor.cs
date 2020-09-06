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

        public Task<Meetup> Create(CreateMeetupModel model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Meetup> Get()
        {
            throw new NotImplementedException();
        }

        public Meetup Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Meetup> Update(int id, UpdateMeetupModel model)
        {
            throw new NotImplementedException();
        }
    }
}
