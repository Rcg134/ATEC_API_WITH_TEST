
using Microsoft.AspNetCore.Mvc.Testing;
using System;


namespace ATEC_API_Test.StagingIntegrationTest
{
    public class StagingAPITest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public StagingAPITest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }


        [Theory]
        [InlineData("DUMMYLOT.1-A", 200)]
        [InlineData("", 400)]
        public async Task IStagingRepository_IsTrackOut_MustReturn_StatusCode(string LotAlias,
                                                                              int StatusCode)
        {
            var client = _factory.CreateDefaultClient();
            client.DefaultRequestHeaders.Add("paramLotAlias", LotAlias);

            var response = await client.GetAsync("/api/Staging/IsTrackOut");
            Assert.Equal(StatusCode, (int)response.StatusCode);
        }



    }
}