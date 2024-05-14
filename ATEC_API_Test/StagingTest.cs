using Moq;
using ATEC_API.Controllers;
using ATEC_API.Data.DTO.StagingDTO;
using ATEC_API.Data.IRepositories;
using ATEC_API.GeneralModels;
using ATEC_API.GeneralModels.MESATECModels.StagingResponse;
using Microsoft.AspNetCore.Mvc;

namespace ATEC_API_Test
{
    public class StagingTest
    {

        public Mock<IStagingRepository> _stagingMock = new();


        [Fact]
        public async Task Check_If_CurrentStage_Is_Already_TrackedOut()
        {

            var expectedResponse = new StagingResponse
            {
                HasSetUp = true,
                IsTrackout = false
            };

            _stagingMock
                  .Setup(repo => repo.IsTrackOut(It.IsAny<StagingDTO>()))
                  .ReturnsAsync(expectedResponse);

            var StagingController = new StagingController(_stagingMock.Object);

            var response = await StagingController.IsLotTrackOut("Sample Lot");

            var okResult = Assert.IsType<OkObjectResult>(response);
            var generalResponse = Assert.IsType<GeneralResponse>(okResult.Value);
            var stagingResponse = Assert.IsType<StagingResponse>(generalResponse.Details);


            Assert.True(stagingResponse.HasSetUp);
            Assert.False(stagingResponse.IsTrackout);

        }
    }
}