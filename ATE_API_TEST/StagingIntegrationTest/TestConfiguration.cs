
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ATEC_API_Test.StagingIntegrationTest
{
    public class TestConfiguration : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public TestConfiguration(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateDefaultClient();
        }
    }
}