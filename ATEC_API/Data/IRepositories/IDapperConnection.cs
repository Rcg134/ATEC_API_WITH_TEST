
using Microsoft.Data.SqlClient;

namespace ATEC_API.Data.IRepositories
{
    public interface IDapperConnection
    {
        SqlConnection MES_ATEC_CreateConnection();

    }
}