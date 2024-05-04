using ATEC_API.Data.Context;
using ATEC_API.Data.DTO;
using ATEC_API.Data.IRepositories;
using ATEC_API.GeneralModels;
using Microsoft.AspNetCore.Mvc;

namespace ATEC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperatorDetailsController : ControllerBase
    {
        private readonly IHRISRepository _hRISRepository;

        public OperatorDetailsController(IHRISRepository hRISRepository)
        {
            _hRISRepository = hRISRepository;
        }

        [HttpPost("IsEmployeeQualified")]
        public async Task<IActionResult> IsEmployeeQualified([FromBody] HRISDTO hRISDTO)
        {
            var isQualified = await _hRISRepository.IsOperatorQualified(hRISDTO);

            return Ok(new GeneralResponse
            {
                Details = isQualified,
            });
        }
    }
}