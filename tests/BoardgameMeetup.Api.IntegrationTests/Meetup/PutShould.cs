using BoardgameMeetup.Api.IntegrationTests.Common;
using BoardgameMeetup.Api.IntegrationTests.Helpers;
using BoardgameMeetup.Api.Models.Meetup;
using FluentAssertions;
using System;
using Xunit;

namespace BoardgameMeetup.Api.IntegrationTests.Meetups
{
    [Collection("ApiCollection")]
    public class PutShould
    {
        private readonly ApiServer _server;
        private readonly HttpClientWrapper _client;

        public PutShould(ApiServer server)
        {
            _server = server;
            _client = new HttpClientWrapper(_server.Client);
        }

        [Fact]
        public async Task UpdateExisting()
        {
            var meetup = await new PostShould(_server).CreateNew();

            var model = new UpdateMeetupModel
            {
                Title = "Title",
                Description = "Description",
                Date = DateTime.UtcNow,
                ParticipantCount = 4,
                Place = "Place",
                Plz = 9999
            };

            await _client.PutAsync<MeetupModel>($"api/Meetup/{meetup.Id}", model);

            var updatedMeetup = await GetItemShould.GetById(_client.Client, meetup.Id);
            updatedMeetup.Title.Should().Be(model.Title);
            updatedMeetup.Description.Should().Be(model.Description);
            updatedMeetup.Date.Should().Be(model.Date);
            updatedMeetup.ParticipantCount.Should().Be(model.ParticipantCount);
            updatedMeetup.Place.Should().Be(model.Place);
            updatedMeetup.Plz.Should().Be(model.Plz);
        }
    }
}
