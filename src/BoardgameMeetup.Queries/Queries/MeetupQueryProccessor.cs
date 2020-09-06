using BoardgameMeetup.Api.Models.Meetup;
using BoardgameMeetup.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgameMeetup.Queries.Queries
{
    class MeetupQueryProccessor : IMeetupQueryProcessor
    {
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
