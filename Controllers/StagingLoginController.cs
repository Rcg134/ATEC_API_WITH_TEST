// <copyright file="LoginController.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Controllers
{
    using ATEC_API.Data.DTO.LoginDTO;
    using ATEC_API.Data.IRepositories;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class StagingLoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public StagingLoginController(ILoginRepository loginRepository)
        {
            this._loginRepository = loginRepository;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
        {
            var userDetails = await this._loginRepository.Login(loginDTO);

            return this.Ok(userDetails);
        }
    }
}
