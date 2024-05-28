using System;
using System.Data;
using ATEC_API.Data.DTO.StagingDTO;
using ATEC_API.Data.IRepositories;
using ATEC_API.Data.StoredProcedures;
using ATEC_API.GeneralModels.MESATECModels.StagingResponse;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ATEC_API.Data.Repositories
{
    public class StagingRepository : IStagingRepository
    {
        private readonly IDapperConnection _dapperConnection;

        public StagingRepository(IDapperConnection dapperConnection)
        {
            _dapperConnection = dapperConnection;
        }
        public async Task<StagingResponse> IsTrackOut(StagingDTO stagingDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var IsTrackOut = await sqlConnection.QueryFirstOrDefaultAsync<StagingResponse>(
                                                                   StagingSP.usp_Staging_IsTrackOut_Test,
                                                                   new
                                                                   {
<<<<<<< HEAD
                                                                       LotAlias = stagingDTO.LotAlias,

=======
                                                                       LotAlias = stagingDTO.lotCode,
>>>>>>> 8ac8438c0d17d168b5b8066d631490bf847f1168
                                                                   },
                                                                   commandType: CommandType.StoredProcedure
                                                                   );
            if (IsTrackOut == null)
            {
                return null;
            }

            return IsTrackOut;
        }
    }
}