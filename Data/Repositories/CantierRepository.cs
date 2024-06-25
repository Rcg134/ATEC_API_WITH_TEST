using System;
using System.Collections;
using System.Data;
using ATEC_API.Data.DTO.Cantier;
using ATEC_API.Data.IRepositories;
using ATEC_API.Data.StoredProcedures;
using ATEC_API.GeneralModels.MESATECModels.CantierResponse;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ATEC_API.Data.Repositories
{
    public class CantierRepository : ICantierRepository
    {
        private readonly IDapperConnection _dapperConnection;

        public CantierRepository(IDapperConnection dapperConnection)
        {
            _dapperConnection = dapperConnection;
        }

        public async Task<IEnumerable<CantierResponse>>? GetLotDetails(CantierDTO cantierDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var LotDetails = await sqlConnection.QueryAsync<CantierResponse>(
                                                                    CantierSP.usp_CANTIER_GetLotTrackInOut,
                                                                    new
                                                                    {
                                                                        LotNumber = cantierDTO.LotNumber,
                                                                        Mode = 3
                                                                    },
                                                                    commandType: CommandType.StoredProcedure
                                                                    );
            return LotDetails;
        }

        public async Task<CantierResponse>? GetTrackInDetails(CantierDTO cantierDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var LotDetails = await sqlConnection.QueryFirstOrDefaultAsync<CantierResponse>(
                                                                    CantierSP.usp_CANTIER_GetLotTrackInOut,
                                                                    new
                                                                    {
                                                                        LotNumber = cantierDTO.LotNumber,
                                                                        Mode = 1
                                                                    },
                                                                    commandType: CommandType.StoredProcedure
                                                                    );

            return LotDetails;
        }

        public async Task<CantierResponse>? GetTrackOutDetails(CantierDTO cantierDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var LotDetails = await sqlConnection.QueryFirstOrDefaultAsync<CantierResponse>(
                                                                    CantierSP.usp_CANTIER_GetLotTrackInOut,
                                                                    new
                                                                    {
                                                                        LotNumber = cantierDTO.LotNumber,
                                                                        Mode = 2
                                                                    },
                                                                    commandType: CommandType.StoredProcedure
                                                                    );

            return LotDetails;
        }
    }
}
