using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATEC_API.Data.IRepositories;
using ATEC_API.Data.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ATEC_API.ExtentionServices
{
    internal sealed class DatabaseHealthCheck : IHealthCheck
    {

        private readonly IDapperConnection _dapperConnection;

        public DatabaseHealthCheck(IDapperConnection dapperConnection)
        {
            _dapperConnection = dapperConnection;
        }

 
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();


                var sql = "SELECT 1";
                var result = await sqlConnection.ExecuteScalarAsync(sql);

                return HealthCheckResult.Healthy("Database is healthy.");
   
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Database health check failed.", ex);
            }
        }
    }
}