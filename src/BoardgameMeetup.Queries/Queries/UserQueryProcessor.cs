using BoardgameMeetup.Api.Models.User;
using BoardgameMeetup.Data.Access.DAL;
using BoardgameMeetup.Data.Models;
using BoardgameMeetup.Security;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoardgameMeetup.Queries.Queries
{
    public class UserQueryProcessor : IUserQueryProcessor
    {
        private readonly IUnitOfWork _unitOfOfWork;
        private readonly ISecurityContext _securityContext;

        public UserQueryProcessor(IUnitOfWork unitOfWork, ISecurityContext securityContext)
        {
            _unitOfOfWork = unitOfWork;
            _securityContext = securityContext;
        }

        public Task<User> Create(CreateUserModel model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> Get()
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(Guid id, UpdateUserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
