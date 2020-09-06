using BoardgameMeetup.Api.Common.Exceptions;
using BoardgameMeetup.Api.Models.Meetup;
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
    public class MeetupQueryProcessorTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private List<Meetup> _meetupList;
        private IMeetupQueryProcessor _query;
        private User _currentUser;
        private Mock<ISecurityContext> _secutityContext;

        public MeetupQueryProcessorTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _meetupList = new List<Meetup>();
            _unitOfWork.Setup(x => x.Query<Meetup>())
                .Returns(() => _meetupList.AsQueryable());

            _currentUser = new User
            {
                Id = Guid.NewGuid()
            };

            _secutityContext = new Mock<ISecurityContext>(MockBehavior.Strict);
            _secutityContext.Setup(x => x.User).Returns(_currentUser);
            _secutityContext.Setup(x => x.IsAdministrator).Returns(false);

            _query = new MeetupQueryProccessor(_unitOfWork.Object,
                _secutityContext.Object);
        }

        public class Get : MeetupQueryProcessorTests
        {
            [Fact]
            [Description("Returns all meetups")]
            public void ReturnAll()
            {
                //Arrange
                _meetupList.Add(new Meetup
                {
                    UserId = _currentUser.Id
                });

                _meetupList.Add(new Meetup
                {
                    UserId = Guid.NewGuid()
                });

                //Act
                var result = _query.Get();

                //Assert
                result.Count().Should().Be(2);
            }

            [Fact]
            [Description("Returns meetups only of a specific user")]
            public void ReturnByUser()
            {
                //Arrange
                _meetupList.Add(new Meetup
                {
                    UserId = _currentUser.Id
                });

                _meetupList.Add(new Meetup
                {
                    UserId = Guid.NewGuid()
                });

                //Act
                var result = _query.Get().ToList();

                //Assert
                result.Count().Should().Be(1);
                result[0].UserId.Should().Be(_currentUser.Id);
            }

            [Fact]
            [Description("Returns meetups that are not passed yet")]
            public void ReturnAllExceptPassed()
            {
                //Arrange
                _meetupList.Add(new Meetup
                {
                    UserId = _currentUser.Id,
                    Date = DateTime.UtcNow.AddDays(1)
                });

                _meetupList.Add(new Meetup
                {
                    UserId = _currentUser.Id,
                    Date = DateTime.UtcNow.AddDays(-1)
                });

                //Act
                var result = _query.Get();

                //Assert
                result.Count().Should().Be(1);
            }

            [Fact]
            [Description("Returns meetup by meetup Id")]
            public void ReturnById()
            {
                //Arrange
                var meetup = new Meetup
                {
                    Id = Guid.NewGuid(),
                    UserId = _currentUser.Id
                };

                _meetupList.Add(meetup);

                _meetupList.Add(new Meetup
                {
                    UserId = Guid.NewGuid()
                });

                //Act
                var result = _query.Get(meetup.Id);

                //Assert
                result.Should().Be(meetup);
            }

            [Fact]
            [Description("Throws exception if no meetup was found by the passed id")]
            public void ThrowExceptionIfItemIsNotFoundById()
            {
                //Arrange
                var meetup = new Meetup
                {
                    Id = Guid.NewGuid(),
                    UserId = _currentUser.Id
                };

                _meetupList.Add(meetup);

                
                Action get = () =>
                {
                    _query.Get(Guid.NewGuid());
                };

                //Act and Assert
                get.Should().Throw<NotFoundException>();
            }
        }

        public class Create : MeetupQueryProcessorTests
        {
            [Fact]
            [Description("Saves new meetup correctly")]
            public async Task SaveNew()
            {
                //Arrange
                var model = new CreateMeetupModel
                {
                    Title = "Title",
                    Description = "Description",
                    Date = DateTime.UtcNow,
                    ParticipantCount = 4,
                    Place = "Place",
                    Plz = 9999
                };

                //Act
                var result = await _query.Create(model);

                //Assert
                result.Title.Should().Be(model.Title);
                result.Description.Should().Be(model.Description);
                result.Date.Should().Be(model.Date);
                result.ParticipantCount.Should().Be(model.ParticipantCount);
                result.Place.Should().Be(model.Place);
                result.Plz.Should().Be(model.Plz);

                _unitOfWork.Verify(x => x.Add(result));
                _unitOfWork.Verify(x => x.CommitAsync());
            }
        }

        public class Update : MeetupQueryProcessorTests
        {
            [Fact]
            [Description("Updates an existing meetup correctly")]
            public async Task FieldsCorrect()
            {
                //Arrange
                var meetup = new Meetup
                {
                    Id = Guid.NewGuid(),
                    UserId = _currentUser.Id
                };
                _meetupList.Add(meetup);

                var model = new UpdateMeetupModel
                {
                    Title = "Title",
                    Description = "Description",
                    Date = DateTime.UtcNow,
                    ParticipantCount = 4,
                    Place = "Place",
                    Plz = 9999
                };

                //Act
                var result = await _query.Update(meetup.Id, model);

                //Assert
                result.Should().Be(meetup);

                result.Title.Should().Be(model.Title);
                result.Description.Should().Be(model.Description);
                result.Date.Should().Be(model.Date);
                result.ParticipantCount.Should().Be(model.ParticipantCount);
                result.Place.Should().Be(model.Place);
                result.Plz.Should().Be(model.Plz);

                _unitOfWork.Verify(x => x.CommitAsync());
            }

            [Fact]
            [Description("Throws exception if meetup with passed id not found on update")]
            public void ThrowExceptionIfItemIsNotFound()
            {
                //Arrange
                Action update = () =>
                {
                    var result = _query.Update(Guid.NewGuid(),
                        new UpdateMeetupModel()).Result;
                };

                //Act and Assert
                update.Should().Throw<NotFoundException>();
            }
        }
    }
}
