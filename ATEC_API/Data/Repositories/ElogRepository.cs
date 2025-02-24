using System;
using System.Collections;
using System.Data;
using ATEC_API.Data.IRepositories;
using ATEC_API.GeneralModels.MESATECModels.ElogResponse;
using ATEC_API.Data.DTO.ElogSheetDTO;
using Dapper;
using Microsoft.Data.SqlClient;
using ATEC_API.Data.StoredProcedures;

namespace ATEC_API.Data.Repositories
{
    public class ElogRepository : IElogRepository
    {
        private readonly IDapperConnection _dapperConnection;

        public ElogRepository(IDapperConnection dapperConnection)
        {
            _dapperConnection = dapperConnection;
        }
        public async Task<ElogResponse>? GetLotDetails(ELogSheetDTO eLogSheetDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();


            var LotDetails = await sqlConnection.QueryFirstOrDefaultAsync<ElogResponse>(
                                                                    ElogSP.usp_GetElog_LotDetails,
                                                                    new
                                                                    {
                                                                        LOTALIAS = eLogSheetDTO.Lotnumber
                                                                    },
                                                                    commandType: CommandType.StoredProcedure
                                                                    );

            return LotDetails;
        }
    }
}
