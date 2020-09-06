using BoardgameMeetup.Api.Models.Meetup;
using BoardgameMeetup.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BoardgameMeetup.Queries.Queries
{
    public interface IMeetupQueryProcessor
    {
        IQueryable<Meetup> Get();
        Meetup Get(int id);
        Task<Meetup> Create(CreateMeetupModel model);
        Task<Meetup> Update(int id, UpdateMeetupModel model);
        Task Delete(int id);
    }
}
