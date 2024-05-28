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
<<<<<<< HEAD
        public async Task<IActionResult> IsEmployeeQualified([FromHeader] string paramLotAlias)
        {
            var staging = new StagingDTO
            {
                LotAlias = paramLotAlias,
=======
        public async Task<IActionResult> IsLotTrackOut([FromHeader] string paramLotAlias)
        {
            var staging = new StagingDTO
            {
                lotCode = paramLotAlias,
>>>>>>> 8ac8438c0d17d168b5b8066d631490bf847f1168
            };

            var isTrackOut = await _stagingRepository.IsTrackOut(staging);

            return Ok(new GeneralResponse
            {
                Details = isTrackOut,
            });
        }

    }
}