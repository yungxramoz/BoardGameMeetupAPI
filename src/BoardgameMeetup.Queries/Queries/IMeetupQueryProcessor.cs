using BoardgameMeetup.Api.Models.Meetup;
using BoardgameMeetup.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoardgameMeetup.Queries.Queries
{
    public interface IMeetupQueryProcessor
    {
        IQueryable<Meetup> Get();
        Meetup Get(Guid id);
        Task<Meetup> Create(CreateMeetupModel model);
        Task<Meetup> Update(Guid id, UpdateMeetupModel model);
        Task Delete(Guid id);
    }
}
