using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
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

        public async Task<IEnumerable<MaterialCustomerResponse>>? GetCustomerHistory(MaterialStagingHistoryDTO materialStaging)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var CustomerHistory = await sqlConnection.QueryAsync<MaterialCustomerResponse>(
                                                                        StagingSP.usp_Material_History,
                                                                        new
                                                                        {
                                                                            MaterialType = materialStaging.MaterialType,
                                                                            Mode = materialStaging.Mode
                                                                        },
                                                                        commandType: CommandType.StoredProcedure
                                                                        );
            return CustomerHistory;
        }

        public async Task<IEnumerable<MaterialStagingHistoryResponse>>? GetMaterialHistory(MaterialStagingHistoryDTO materialHistory)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var MaterialHistory = await sqlConnection.QueryAsync<MaterialStagingHistoryResponse>(
                                                                        StagingSP.usp_Material_History,
                                                                        new
                                                                        {
                                                                            MaterialType = materialHistory.MaterialType,
                                                                            CustomerCode = materialHistory.CustomerCode,
                                                                            DateFrom = materialHistory.DateFrom,
                                                                            DateTo = materialHistory.DateTo,
                                                                            Mode = materialHistory.Mode
                                                                        },
                                                                        commandType: CommandType.StoredProcedure
                                                                        );
            return MaterialHistory;
        }



        public async Task<IEnumerable<MaterialCustomerResponse>>? GetMaterialCustomer(int paramMaterialType)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var CustomerAvailable = await sqlConnection.QueryAsync<MaterialCustomerResponse>(
                                                                            StagingSP.usp_Material_Customer,
                                                                            new
                                                                            {
                                                                                MaterialType = paramMaterialType
                                                                            },
                                                                            commandType: CommandType.StoredProcedure
                                                                            );
            return CustomerAvailable;
        }

        public async Task<IEnumerable<MaterialStagingResponse>>? GetMaterialDetail(MaterialStagingDTO materialStagingDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var MaterialDetails = await sqlConnection.QueryAsync<MaterialStagingResponse>(
                                                                            StagingSP.usp_Material_Details,
                                                                            new
                                                                            {
                                                                                SID = materialStagingDTO.Sid,
                                                                                MaterialID = materialStagingDTO.MaterialId,
                                                                                Serial = materialStagingDTO.Serial,
                                                                                ExpirationDate = materialStagingDTO.ExpirationDate,
                                                                                CustomerCode = materialStagingDTO.CustomerCode,
                                                                                MaterialType = materialStagingDTO.MaterialType,
                                                                                Usercode = materialStagingDTO.Usercode
                                                                            },
                                                                            commandType: CommandType.StoredProcedure
                                                                            );
            return MaterialDetails;
        }

        public async Task<IEnumerable<MaterialStagingResponse>>? GetMaterialDetailNew(MaterialStagingNewDTO materialStagingNewDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var MaterialDetails = await sqlConnection.QueryAsync<MaterialStagingResponse>(
                StagingSP.usp_Material_InOut_Endpoint,
                new
                {
                    SID = materialStagingNewDTO.Sid,
                    MaterialId = materialStagingNewDTO.MaterialId,
                    Serial = materialStagingNewDTO.Serial,
                    ExpirationDate = materialStagingNewDTO.ExpirationDate,
                    MaterialType = materialStagingNewDTO.MaterialType,
                    Usercode = materialStagingNewDTO.Usercode
                },
                commandType: CommandType.StoredProcedure
                );
            return MaterialDetails;
        }

        public async Task<IEnumerable<MaterialStagingResponse>>? GetMaterialLotMachine(MaterialStagingLMDTO materialStagingLMDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var LMDetails = await sqlConnection.QueryAsync<MaterialStagingResponse>(
                StagingSP.usp_Material_LotMachine_Endpoint,
                new
                {
                    LotAlias = materialStagingLMDTO.LotAlias,
                    MachineNo = materialStagingLMDTO.MachineNo,
                    Mode = materialStagingLMDTO.Mode,
                    SID = materialStagingLMDTO.Sid,
                    MaterialId = materialStagingLMDTO.MaterialId,
                    Serial = materialStagingLMDTO.Serial
                },
                commandType: CommandType.StoredProcedure
                );
            return LMDetails;
        }

        public async Task<StagingResponse> IsTrackOut(StagingDTO stagingDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var IsTrackOut = await sqlConnection.QueryFirstOrDefaultAsync<StagingResponse>(
                                                                   StagingSP.usp_Staging_IsTrackOut_Test,
                                                                   new
                                                                   {
                                                                       LotAlias = stagingDTO.LotAlias,

                                                                   },
                                                                   commandType: CommandType.StoredProcedure
                                                                   );
            if (IsTrackOut == null)
            {
                return null;
            }

            return IsTrackOut;
        }

        public async Task<IEnumerable<MaterialStagingResponse>>? CheckLotNumber(MaterialStagingCheckParamDTO materialStaging)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var LotExist = await sqlConnection.QueryAsync<MaterialStagingResponse>(
                                                                    StagingSP.usp_Check_Param,
                                                                    new
                                                                    {
                                                                        LotAlias = materialStaging.LotNumber,
                                                                        MachineNo = materialStaging.Machine,
                                                                        CustomerCode = materialStaging.CustomerCode,
                                                                        Mode = materialStaging.Mode,
                                                                        SID = materialStaging.SID,
                                                                        MaterialId = materialStaging.MaterialId,
                                                                        Serial = materialStaging.Serial
                                                                    },
                                                                    commandType: CommandType.StoredProcedure
                                                                    );
            return LotExist;
        }

        public async Task<IEnumerable<MaterialStagingResponse>>? GetMaterialDetailPG(MaterialStagingNewDTO materialStagingNewDTO)
        {
            await using SqlConnection sqlConnection = _dapperConnection.MES_ATEC_CreateConnection();

            var MaterialDetails = await sqlConnection.QueryAsync<MaterialStagingResponse>(
                StagingSP.usp_Material_InOut_PG,
                new
                {
                    SID = materialStagingNewDTO.Sid,
                    MaterialId = materialStagingNewDTO.MaterialId,
                    Serial = materialStagingNewDTO.Serial,
                    ExpirationDate = materialStagingNewDTO.ExpirationDate,
                    MaterialType = materialStagingNewDTO.MaterialType,
                    Usercode = materialStagingNewDTO.Usercode
                },
                commandType: CommandType.StoredProcedure
                );
            return MaterialDetails;
        }
    }
}