using System.Text.Json;
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
        private readonly ILogger<StagingController> _logger;

        public StagingController(IStagingRepository stagingRepository ,
                                 ILogger<StagingController> logger)
        {
            _logger = logger;
            _stagingRepository = stagingRepository;
        }

        [HttpGet("IsTrackOut")]
        public async Task<IActionResult> IsLotTrackOut([FromHeader] string paramLotAlias)
        {
            _logger.LogInformation($"Invoking IsLotTrackOut method with {paramLotAlias} parameters");

            var staging = new StagingDTO
            {
                LotAlias = paramLotAlias,
            };

            var isTrackOut = await _stagingRepository.IsTrackOut(staging);


            _logger.LogInformation($"{paramLotAlias} details are {JsonSerializer.Serialize(isTrackOut)}");

            return Ok(new GeneralResponse
            {
                Details = isTrackOut,
            });
        }

    }
}