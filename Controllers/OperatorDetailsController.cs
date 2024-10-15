// <copyright file="OperatorDetailsController.cs" company="ATEC">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATEC_API.Controllers
{
    using ATEC_API.Data.DTO.HRISDTO;
    using ATEC_API.Data.IRepositories;
    using ATEC_API.GeneralModels;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class OperatorDetailsController(IHRISRepository hRISRepository): ControllerBase
    {
        private readonly IHRISRepository _hRISRepository = hRISRepository;

        [HttpGet("IsEmployeeQualified")]

        public async Task<IActionResult> IsEmployeeQualified([FromHeader] string paramEmpNo,
                                                             [FromHeader] string paramCustomerId,
                                                             [FromHeader] int paramRecipeCode , CancellationToken cancellationToken)
        {
            var hrisObject = new HRISDTO
            {
                EmpNo = paramEmpNo,
                CustomerId = paramCustomerId,
                RecipeCode = paramRecipeCode,
            };


            var isQualified = await this._hRISRepository.IsOperatorQualified(hrisObject,cancellationToken);

            return this.Ok(new GeneralResponse
            {
                Details = isQualified,
            });
        }
    }
}