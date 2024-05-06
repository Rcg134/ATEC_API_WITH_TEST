using ATEC_API.Data.DTO.HRISDTO;
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
        public async Task<IActionResult> IsEmployeeQualified([FromHeader] string paramEmpNo,
                                                             [FromHeader] string paramCustomerId,
                                                             [FromHeader] int paramRecipeCode)
        {
            var hrisObject = new HRISDTO
            {
                EmpNo = paramEmpNo,
                CustomerId = paramCustomerId,
                RecipeCode = paramRecipeCode
            };


            var isQualified = await _hRISRepository.IsOperatorQualified(hrisObject);

            return Ok(new GeneralResponse
            {
                Details = isQualified,
            });
        }
    }
}