using BoardgameMeetup.Api.IntegrationTests.Common;
using BoardgameMeetup.Api.Models.Common;
using BoardgameMeetup.Api.Models.Meetup;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BoardgameMeetup.Api.IntegrationTests.Meetups
{
    [Collection("ApiCollection")]
    public class GetListShould
    {
        private readonly ApiServer _server;
        private readonly HttpClient _client;

        public GetListShould(ApiServer server)
        {
            _server = server;
            _client = server.Client;
        }

        public static async Task<DataResult<MeetupModel>> Get(HttpClient client)
        {
            var response = await client.GetAsync($"api/Meetup");
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            var meetups = JsonConvert.DeserializeObject<DataResult<MeetupModel>>(responseText);
            return meetups;
        }

        [Fact]
        public async Task ReturnAnyList()
        {
            var meetups = await Get(_client);
            meetups.Should().NotBeNull();
        }
    }
}
