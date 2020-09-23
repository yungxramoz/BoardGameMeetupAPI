using BoardgameMeetup.Api.Common.Exceptions;
using BoardgameMeetup.Api.Models.User;
using BoardgameMeetup.Data.Access.DAL;
using BoardgameMeetup.Data.Models;
using BoardgameMeetup.Queries.Queries;
using BoardgameMeetup.Security;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BoardgameMeetup.Queries.Tests
{
    public class UserQueryProcessorTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private List<User> _userList;
        private IUserQueryProcessor _query;
        private User _currentUser;
        private Mock<ISecurityContext> _secutityContext;

        public UserQueryProcessorTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _userList = new List<User>();
            _unitOfWork.Setup(x => x.Query<User>())
                .Returns(() => _userList.AsQueryable());

            _currentUser = new User
            {
                Id = Guid.NewGuid()
            };

            _secutityContext = new Mock<ISecurityContext>(MockBehavior.Strict);
            _secutityContext.Setup(x => x.User).Returns(_currentUser);
            _secutityContext.Setup(x => x.IsAdministrator).Returns(false);

            _query = new UserQueryProcessor(_unitOfWork.Object,
                _secutityContext.Object);
        }

        public class Get : UserQueryProcessorTests
        {
            [Fact]
            [Description("Returns all users")]
            public void ReturnAll()
            {
                //Arrange
                _userList.Add(new User());
                _userList.Add(new User());

                //Act
                var result = _query.Get();

                //Assert
                result.Count().Should().Be(2);
            }

            [Fact]
            [Description("Returns all users except the deleted users")]
            public void ReturnAllExpectDeleted()
            {
                //Arrange
                _userList.Add(new User());
                _userList.Add(new User());
                _userList.Add(new User
                {
                    IsDeleted = true
                });

                //Act
                var result = _query.Get();

                //Assert
                result.Count().Should().Be(2);
            }

            [Fact]
            [Description("Returns specific user by passed id")]
            public void ReturnById()
            {
                //Arrange
                var user = new User
                {
                    Id = Guid.NewGuid()
                };

                _userList.Add(user);
                _userList.Add(new User());

                //Act
                var result = _query.Get(user.Id);

                //Assert
                result.Should().Be(user);
            }

            [Fact]
            [Description("Throws exception if no user was found by the passed id")]
            public void ThrowExceptionIfItemIsNotFoundById()
            {
                //Arrange
                _userList.Add(new User());

                //Act
                Action get = () =>
                {
                    _query.Get(Guid.NewGuid());
                };

                //Assert
                get.Should().Throw<NotFoundException>();
            }

            [Fact]
            [Description("Throws exception if user with passed id is deleted")]
            public void ThrowExceptionIfUserIsDeleted()
            {
                //Arrange
                var user = new User
                {
                    IsDeleted = true
                };

                _userList.Add(user);

                //Act
                Action get = () =>
                {
                    _query.Get(user.Id);
                };

                //Assert
                get.Should().Throw<NotFoundException>();
            }
        }
        
        public class Create: UserQueryProcessorTests
        {
            [Fact]
            [Description("Saves new user correctly")]
            public async Task SaveNew()
            {
                //Arrange
                var model = new CreateUserModel
                {
                    Username = "user",
                    FirstName = "Unito",
                    LastName = "Testimus",
                    Password = "SuperSafePW123"
                };

                //Act
                var result = await _query.Create(model);

                //Assert
                result.Username.Should().Be(model.Username);
                result.FirstName.Should().Be(model.FirstName);
                result.LastName.Should().Be(model.LastName);
                //should not be password itself
                result.Password.Should().NotBeEmpty();
                result.Password.Should().NotBe(model.Password);

                 _unitOfWork.Verify(x => x.Add(result));
                _unitOfWork.Verify(x => x.CommitAsync());
            }

            [Fact]
            public void ThrowExceptionIfUsernameIsNotUnique()
            {
                var model = new CreateUserModel
                {
                    Username = "user",
                };

                _userList.Add(new User { Username = model.Username });

                Action create = () =>
                {
                    var x = _query.Create(model).Result;
                };

                create.Should().Throw<BadRequestException>();
            }
        }

        public class Update : UserQueryProcessorTests
        {
            [Fact]
            [Description("Updates an existing user correctly")]
            public async Task FieldsCorrect()
            {
                //Arrange
                var user = new User
                {
                    Id = Guid.NewGuid(),
                };
                _userList.Add(user);

                var model = new UpdateUserModel
                {
                    Username = "user",
                    FirstName = "Unito",
                    LastName = "Testimus"
                };

                //Act
                var result = await _query.Update(user.Id, model);

                //Assert
                result.Should().Be(user);

                result.Username.Should().Be(model.Username);
                result.FirstName.Should().Be(model.FirstName);
                result.LastName.Should().Be(model.LastName);

                _unitOfWork.Verify(x => x.CommitAsync());
            }

            [Fact]
            [Description("Throws exception if user with passed id not found on update")]
            public void ThrowExceptionIfUserNotFound()
            {
                //Arrange
                Action update = () =>
                {
                    var result = _query.Update(Guid.NewGuid(),
                        new UpdateUserModel()).Result;
                };

                //Act and Assert
                update.Should().Throw<NotFoundException>();
            }
        }

        public class Delete: UserQueryProcessorTests
        {
            [Fact]
            [Description("Marks a user as deleted")]
            public async Task MarkAsDeleted()
            {
                //Arrange
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false
                };

                _userList.Add(user);

                //Act
                await _query.Delete(user.Id);

                //Assert
                user.IsDeleted.Should().BeTrue();

                _unitOfWork.Verify(x => x.CommitAsync());
            }

            [Fact]
            [Description("Throws exception if user to delete does not exist anymore")]
            public void ThrowExceptionIfUserIsNotFound()
            {
                //Act
                Action delete = () =>
                {
                    _query.Delete(Guid.NewGuid()).Wait();
                };

                //Act & Assert
                delete.Should().Throw<NotFoundException>();
            }
        }

        //TODO change password tests
    }
}
