using ATEC_API.Data.DTO.StagingDTO;
using ATEC_API.Data.IRepositories;
using ATEC_API.GeneralModels;
using Microsoft.AspNetCore.Mvc;


namespace ATEC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StagingController : Controller
    {
        private readonly IStagingRepository _stagingRepository;

        public StagingController(IStagingRepository stagingRepository)
        {
            _stagingRepository = stagingRepository;
        }

        [HttpGet("IsTrackOut")]
        public async Task<IActionResult> IsEmployeeQualified([FromHeader] string paramLotCode,
                                                             [FromHeader] int paramCurrentStageCode,
                                                             [FromHeader] int paramNextStageCode)
        {
            var staging = new StagingDTO
            {
                lotCode = paramLotCode,
                currentStationId = paramCurrentStageCode,
                nextStationId = paramNextStageCode
            };

            var isTrackOut = await _stagingRepository.IsTrackOut(staging);

            return Ok(new GeneralResponse
            {
                Details = isTrackOut,
            });
        }

    }
}