using BoardgameMeetup.Api.IntegrationTests.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BoardgameMeetup.Api.IntegrationTests.Meetups
{
    [Collection("ApiCollection")]
    public class DeleteShould
    {
        private readonly ApiServer _server;
        private readonly HttpClient _client;

        public DeleteShould(ApiServer server)
        {
            _server = server;
            _client = server.Client;
        }

        [Fact]
        public async Task DeleteExistingItem()
        {
            var meetup = await new PostShould(_server).CreateNew();

            var result = await _client.DeleteAsync(new Uri($"api/Meetup/{meetup.Id}", UriKind.Relative));
            result.EnsureSuccessStatusCode();
        }
    }
}
