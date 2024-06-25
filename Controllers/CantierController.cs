using ATEC_API.Data.DTO.Cantier;
using ATEC_API.Data.IRepositories;
using ATEC_API.GeneralModels;
using Microsoft.AspNetCore.Mvc;

namespace ATEC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CantierController : Controller
    {
        private readonly ICantierRepository _cantierRepository;
        
        public CantierController(ICantierRepository cantierRepository)
        {
            _cantierRepository = cantierRepository;
        }

        [HttpGet("GetLotDetails")]
        public async Task<IActionResult> GetLotDetails([FromHeader] string paramLotNumber)
        {
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
