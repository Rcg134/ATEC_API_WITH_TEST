// <copyright file="DapperModelPagination.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.Service
{
    using ATEC_API.Data.IRepositories;
    using ATEC_API.GeneralModels;
    using Dapper;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Data.SqlClient;
    using System.Data;

    public class DapperModelPagination
    {
        private readonly IDapperConnection _dapperConnection;

        public DapperModelPagination(IDapperConnection dapperConnection)
        {
            this._dapperConnection = dapperConnection;
        }

        public async Task<(IEnumerable<T> accounts,
                           PageResultsResponse pageResultsResponse)>
            GetAccountsAndPagingInfoAsync<T>(String SP,
                                              DynamicParameters parameters)
        {
            await using SqlConnection sqlConnection = _dapperConnection
                      .MES_ATEC_CreateConnection();

            using (var multi = await sqlConnection.QueryMultipleAsync(SP, parameters, commandType: CommandType.StoredProcedure))
            {
                var accounts = await multi.ReadAsync<T>();
                var pagingInfo = (await multi.ReadAsync<PageResultsResponse>()).SingleOrDefault();

                return (accounts, pagingInfo);
            }
        }
    }
}
