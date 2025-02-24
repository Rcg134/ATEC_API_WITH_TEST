namespace ATEC_API.Controllers
{
    using ATEC_API.Data.DTO.ElogSheetDTO;
    using ATEC_API.Data.IRepositories;
    using ATEC_API.GeneralModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ElogSheetController (IElogRepository elogRepository) : ControllerBase
    {
       private readonly IElogRepository _elogRepository;
        [HttpGet("GetLotDetails")]
        public async Task<IActionResult> GetLotDetails([FromHeader] string paramLotnumber)
        {
            var elog = new ELogSheetDTO { Lotnumber = paramLotnumber };
            var getLotDetails = await elogRepository.GetLotDetails(elog);

            return this.Ok(new GeneralResponse
            {
                Details = getLotDetails
            });
        }
    }
}
