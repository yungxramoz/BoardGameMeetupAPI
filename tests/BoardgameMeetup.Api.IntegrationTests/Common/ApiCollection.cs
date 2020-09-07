using Xunit;

namespace BoardgameMeetup.Api.IntegrationTests.Common
{
    [CollectionDefinition("ApiCollection")]
    public class ApiCollection : ICollectionFixture<ApiServer>
    {
    }
}
