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
        public async Task<Boolean> IsTrackOut(StagingDTO stagingDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var IsTrackOut = await sqlConnection.QueryFirstOrDefaultAsync<StagingResponse>(
                                                                   StagingSP.usp_Staging_IsTrackOut,
                                                                   new
                                                                   {
                                                                       StationId = stagingDTO.stationId,
                                                                       LotCode = stagingDTO.lotCode,
                                                                   },
                                                                   commandType: CommandType.StoredProcedure
                                                                   );

            if (IsTrackOut.IsTrackout == true)
            {
                return true;
            }

            return false;
        }
    }
}