// <copyright file="IDapperConnection.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.IRepositories
{
    using Microsoft.Data.SqlClient;

    public interface IDapperConnection
    {
        SqlConnection MES_ATEC_CreateConnection();

        SqlConnection ATEC_CentralAccess_CreateConnection();

    }
}