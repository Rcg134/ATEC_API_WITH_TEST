using Moq;
using ATEC_API.Controllers;
using ATEC_API.Data.DTO.StagingDTO;
using ATEC_API.Data.IRepositories;
using ATEC_API.GeneralModels;
using ATEC_API.GeneralModels.MESATECModels.StagingResponse;
using Microsoft.AspNetCore.Mvc;

namespace ATEC_API_Test.StagingUnitTest
{
    public class StagingTest
    {

        public Mock<IStagingRepository> _stagingMock = new();

        public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
                new object[] { "Dummy-lot-A", true, true },
                new object[] { "CRA", false, false },
        };


        [Theory]
        [MemberData(nameof(TestData))]
        public async Task Check_If_CurrentStage_Is_Already_TrackedOut(string lotAlias,
                                                                      bool HasSetUp,
                                                                      bool IsTrackOut)
        {
            var expectedResponse = new StagingResponse
            {
                HasSetUp = HasSetUp,
                IsTrackout = IsTrackOut
            };

            _stagingMock
                  .Setup(repo => repo.IsTrackOut(It.IsAny<StagingDTO>()))
                  .ReturnsAsync(expectedResponse);

            var StagingController = new StagingController(_stagingMock.Object);

            var response = await StagingController.IsLotTrackOut(lotAlias);

            var okResult = Assert.IsType<OkObjectResult>(response);
            var generalResponse = Assert.IsType<GeneralResponse>(okResult.Value);
            var stagingResponse = Assert.IsType<StagingResponse>(generalResponse.Details);


            Assert.Equal(HasSetUp, stagingResponse.HasSetUp);
            Assert.Equal(IsTrackOut, stagingResponse.IsTrackout);

            _stagingMock.Verify(repo => repo.IsTrackOut(It.IsAny<StagingDTO>()), Times.Once);

        }
    }
}