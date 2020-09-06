using BoardgameMeetup.Data.Access.DAL;
using BoardgameMeetup.Data.Models;
using BoardgameMeetup.Queries.Queries;
using BoardgameMeetup.Security;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BoardgameMeetup.Queries.Tests
{
    public class MeetupQueryProcessorTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private List<Meetup> _meetupList;
        private IMeetupQueryProcessor _query;
        private Random _random;
        private User _currentUser;
        private Mock<ISecurityContext> _secutityContext;

        public MeetupQueryProcessorTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _random = new Random();

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
    }
}
