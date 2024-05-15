
using ATEC_API.Data.IRepositories;
using Microsoft.Data.SqlClient;

namespace ATEC_API.Data.Repositories
{
    public class DapperConnection : IDapperConnection
    {
        private readonly IConfiguration _configuration;
        private SqlConnection connection;


        public DapperConnection(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public SqlConnection MES_ATEC_CreateConnection()
        {
            return new SqlConnection(
                           _configuration.GetConnectionString("MESATEC_Connection"));
        }
    }
}