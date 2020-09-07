using BoardgameMeetup.Api.IntegrationTests.Common;
using BoardgameMeetup.Api.IntegrationTests.Helpers;
using BoardgameMeetup.Api.Models.Meetup;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BoardgameMeetup.Api.IntegrationTests.Meetups
{
    [Collection("ApiCollection")]
    public class PostShould
    {
        private readonly ApiServer _server;
        private readonly HttpClientWrapper _client;

        public PostShould(ApiServer server)
        {
            _server = server;
            _client = new HttpClientWrapper(server.Client);
        }

        [Fact]
        public async Task<MeetupModel> CreateNew()
        {
            var model = new CreateMeetupModel
            {
                Title = "Title",
                Description = "Description",
                Date = DateTime.UtcNow,
                ParticipantCount = 4,
                Place = "Place",
                Plz = 9999
            };

            var createdMeetup = await _client.PostAsync<MeetupModel>($"api/Meetup", model);
            createdMeetup.Title.Should().Be(model.Title);
            createdMeetup.Description.Should().Be(model.Description);
            createdMeetup.Date.Should().Be(model.Date);
            createdMeetup.ParticipantCount.Should().Be(model.ParticipantCount);
            createdMeetup.Place.Should().Be(model.Place);
            createdMeetup.Plz.Should().Be(model.Plz);
            createdMeetup.Username.Should().Be("admin admin");

            return createdMeetup;
        }
    }
}
