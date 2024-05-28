
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Text.Json;


namespace ATEC_API_Test.StagingIntegrationTest
{
    public class StagingAPITest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;


        public StagingAPITest(WebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/Staging/IsTrackOut");
            _client = factory.CreateClient();
        }


        [Theory]
        [InlineData("DUMMYLOT.1-A", 200)]
        [InlineData("", 400)]
        public async Task IStagingRepository_IsTrackOut_MustReturn_StatusCode(string LotAlias,
                                                                              int StatusCode)
        {
            _client.DefaultRequestHeaders.Add("paramLotAlias", LotAlias);
            var response = await _client.GetAsync("");

            Assert.Equal(StatusCode, (int)response.StatusCode);
        }

        [Fact]
        public async Task IStagingRepository_IsTrackOut_ReturnExpectedMediaType()
        {

            var response = await _client.GetAsync("");
            _client.DefaultRequestHeaders.Add("paramLotAlias", "DUMMYLOT.1-A");

            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task IStagingRepository_IsTrackOut_ReturnMustNotNull()
        {
            var response = await _client.GetAsync("");
            _client.DefaultRequestHeaders.Add("paramLotAlias", "DUMMYLOT.1-A");

            Assert.NotNull(response);
            Assert.True(response.Content.Headers.ContentLength > 0);
        }


        [Fact]
        public async Task IStagingRepository_IsTrackOut_ReturnsExpectedJSON()
        {
            var expectedJson = "{\"details\":{\"hasSetUp\":true,\"isTrackout\":true}}";

            _client.DefaultRequestHeaders.Add("paramLotAlias", "DUMMYLOT.1-A");
            var responseStream = await _client.GetStreamAsync("");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var responseModel = await JsonSerializer.DeserializeAsync<expectedIsTrackOutModel>(responseStream, options);

            var actualJson = JsonSerializer.Serialize(responseModel);

            Assert.NotNull(actualJson);
            Assert.Equal(expectedJson, actualJson);
        }



        //Improve Integration testing using HTTP.JSON that all all for test method in one single method
        //use case insensitive deserialization
        //validate response has success status code
        //validate content media type header
        //Validate response includes header content
        [Fact]
        public async Task Getall_ReturnsExpectedRespomes()
        {
            var expectedJson = "{\"details\":{\"hasSetUp\":true,\"isTrackout\":true}}";
            _client.DefaultRequestHeaders.Add("paramLotAlias", "DUMMYLOT.1-A");
            var responseModel = await _client.GetFromJsonAsync<expectedIsTrackOutModel>("");

            var actualJson = JsonSerializer.Serialize(responseModel);
            Assert.NotNull(responseModel.details);
            Assert.Equal(expectedJson, actualJson);
        }






    }
}