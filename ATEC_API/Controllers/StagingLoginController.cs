// <copyright file="LoginController.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Controllers
{
    using ATEC_API.Data.DTO.LoginDTO;
    using ATEC_API.Data.IRepositories;
    using ATEC_API.GeneralModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Text.Json;

    [ApiController]
    [Route("api/[controller]")]
    public class StagingLoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<StagingLoginController> _ilogger;

        public StagingLoginController(ILoginRepository loginRepository,
                                      ILogger<StagingLoginController> ilogger)
        {
            this._loginRepository = loginRepository;
            this._ilogger = ilogger;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
        {
            this._ilogger.LogInformation("LoginUser method is invoking");

            var userDetails = await this._loginRepository.Login(loginDTO);

            this._ilogger.LogInformation($"Details {JsonSerializer.Serialize(userDetails)}");

            return this.Ok(new GeneralResponse
            {
                Details = userDetails,
            });
        }
    }
}
