// <copyright file="LoginRepository.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.Repositories
{
    using System.Data;
    using ATEC_API.Data.DTO.LoginDTO;
    using ATEC_API.Data.IRepositories;
    using ATEC_API.Data.StoredProcedures;
    using ATEC_API.GeneralModels.LoginModels;
    using Dapper;
    using Microsoft.Data.SqlClient;

    public class LoginRepository : ILoginRepository
    {
        private readonly IDapperConnection _dapperConnection;

        public LoginRepository(IDapperConnection dapperConnection)
        {
            this._dapperConnection = dapperConnection;
        }

        public async Task<LoginResponse> Login(LoginDTO loginDTO)
        {
            await using SqlConnection sqlConnection = this._dapperConnection.ATEC_CentralAccess_CreateConnection();

            var userDetails = await sqlConnection.QueryAsync<LoginResponse>(
                                                                            LoginSP.GetUserByUsernamePassword,
                                                                            new
                                                                            {
                                                                                 username = loginDTO.Username,
                                                                                 password = loginDTO.Password,
                                                                            },
                                                                            commandType: CommandType.StoredProcedure);
            if (userDetails == null || userDetails.Count() == 0)
            {
               var emptyLoginResponse = new LoginResponse();
               return emptyLoginResponse;
            }

            var userDetail = userDetails.SingleOrDefault(details => details.ApplicationID == "Staging");

            if (userDetail == null)
            {
                var emptyLoginResponse = new LoginResponse();
                return emptyLoginResponse;
            }

            return (LoginResponse)userDetail;
        }
    }
}
