// <copyright file="DapperConnection.cs" company="ATEC">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace ATEC_API.Data.Repositories
{
    using ATEC_API.Data.IRepositories;
    using Microsoft.Data.SqlClient;

    public class DapperConnection : IDapperConnection
    {
        private readonly IConfiguration _configuration;
        private SqlConnection? connection;

        public DapperConnection(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public SqlConnection ATEC_CentralAccess_CreateConnection()
        {
            return new SqlConnection(
                        _configuration.GetConnectionString("CentralAccess_Connection"));
        }

        public SqlConnection MES_ATEC_CreateConnection()
        {
            return new SqlConnection(
                           _configuration.GetConnectionString("MESATEC_Connection"));
        }
    }
}