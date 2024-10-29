// <copyright file="ILoginRepository.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.IRepositories
{
    using ATEC_API.Data.DTO.LoginDTO;
    using ATEC_API.GeneralModels.LoginModels;

    public interface ILoginRepository
    {
        Task<LoginResponse> Login(LoginDTO loginDTO);
    }
}
