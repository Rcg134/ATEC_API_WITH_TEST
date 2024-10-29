using ATEC_API.Data.DTO.Cantier;
using ATEC_API.Data.IRepositories;
using ATEC_API.GeneralModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ATEC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CantierController : Controller
    {
        private readonly ICantierRepository _cantierRepository;
        private readonly ILogger<CantierController> _logger;
        
        public CantierController(ICantierRepository cantierRepository,
                                 ILogger<CantierController> logger)
        {
            _logger = logger;
            _cantierRepository = cantierRepository;
        }

        [HttpGet("RecipeLoadDetails")]
        public async Task<IActionResult> RecipeLoadDetails([FromHeader] string paramLotNumber)
        {

            var cantier = new CantierDTO
            {
                LotNumber = paramLotNumber
            };

            var getLotDetails = await _cantierRepository.RecipeLoadDetails(cantier);

            return Ok(new GeneralResponse
            {
                Details = getLotDetails
            });
        }

        [HttpGet("GetLotDetails")]
        public async Task<IActionResult> GetLotDetails([FromHeader] string paramLotNumber)
        {
            _logger.LogInformation($"");


            var cantier = new CantierDTO 
            { 
                LotNumber = paramLotNumber,
            };

            var getLotDetails = await _cantierRepository.GetLotDetails(cantier);

            return Ok(new GeneralResponse
            {
                Details = getLotDetails,
            });
        }

        [HttpGet("GetLotDetailsTrackIn")]
        public async Task<IActionResult> GetLotDetailsTrackIn([FromHeader] string paramLotNumber)
        {
            var cantier = new CantierDTO
            {
                LotNumber = paramLotNumber
            };

            var getTrackInDetails = await _cantierRepository.GetTrackInDetails(cantier);

            return Ok(new GeneralResponse
            {
                Details = getTrackInDetails,
            });
        }

        [HttpGet("GetLotDetailsTrackOut")]
        public async Task<IActionResult> GetLotDetailsTrackOut([FromHeader] string paramLotNumber)
        {
            var cantier = new CantierDTO
            {
                LotNumber = paramLotNumber
            };

            var getTrackInDetails = await _cantierRepository.GetTrackOutDetails(cantier);

            return Ok(new GeneralResponse
            {
                Details = getTrackInDetails,
            });
        }



    }
}
