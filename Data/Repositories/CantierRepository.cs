using System;
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

        public async Task<CantierResponse> GetLotDetails(CantierDTO cantierDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var LotDetails = await sqlConnection.QueryFirstOrDefaultAsync<CantierResponse>(
                                                                    CantierSP.usp_CANTIER_GetLotDetails,
                                                                    new
                                                                    {
                                                                        LotNumber = cantierDTO.LotNumber
                                                                    },
                                                                    commandType: CommandType.StoredProcedure
                                                                    );
            if (LotDetails == null)
            {
                return null;
            }

            return LotDetails;
        }

    }
}
