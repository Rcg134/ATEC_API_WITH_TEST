// <copyright file="DapperModelPagination.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.Service
{
    using System.Data;
    using ATEC_API.Data.IRepositories;
    using ATEC_API.GeneralModels;
    using Dapper;
    using Microsoft.Data.SqlClient;

    public class DapperModelPagination
    {
        private readonly IDapperConnection _dapperConnection;

        public DapperModelPagination(IDapperConnection dapperConnection)
        {
            this._dapperConnection = dapperConnection;
        }

        public async Task<(IEnumerable<T> dataList,
                           PageResultsResponse pageResultsResponse)>
            GetDetailsAndPagingInfoAsync<T>(String SP,
                                             DynamicParameters parameters)
        {
            await using SqlConnection sqlConnection = _dapperConnection
                      .MES_ATEC_CreateConnection();

            using (var multi = await sqlConnection.QueryMultipleAsync(SP, parameters, commandType: CommandType.StoredProcedure))
            {
                var dataList = await multi.ReadAsync<T>();
                var pagingInfo = (await multi.ReadAsync<PageResultsResponse>()).SingleOrDefault();

                return (dataList, pagingInfo);
            }
        }
    }
}
