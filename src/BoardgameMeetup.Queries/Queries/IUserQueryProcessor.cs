using BoardgameMeetup.Api.Models.User;
using BoardgameMeetup.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoardgameMeetup.Queries.Queries
{
    public interface IUserQueryProcessor
    {
        IQueryable<User> Get();
        User Get(Guid id);
        Task<User> Create(CreateUserModel model);
        Task<User> Update(Guid id, UpdateUserModel model);
        Task Delete(Guid id);
    }
}
