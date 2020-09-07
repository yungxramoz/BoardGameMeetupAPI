using BoardgameMeetup.Api.IntegrationTests.Common;
using BoardgameMeetup.Api.Models.Meetup;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BoardgameMeetup.Api.IntegrationTests.Meetups
{
    [Collection("ApiCollection")]
    public class GetItemShould
    {
        private readonly ApiServer _server;
        private readonly HttpClient _client;

        public GetItemShould(ApiServer server)
        {
            _server = server;
            _client = server.Client;
        }

        public static async Task<MeetupModel> GetById(HttpClient client, Guid id)
        {
            var response = await client.GetAsync(new Uri($"api/Meetup/{id}", UriKind.Relative));
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            var meetup = JsonConvert.DeserializeObject<MeetupModel>(responseText);
            return meetup;
        }

        [Fact]
        public async Task ById()
        {
            var meetup = await new PostShould(_server).CreateNew();
            var result = await GetById(_client, meetup.Id);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task NotFoundStatusCode()
        {
            var response = await _client.GetAsync(new Uri($"api/Expenses/-1", UriKind.Relative));
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }
    }
}
