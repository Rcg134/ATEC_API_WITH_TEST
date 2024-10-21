// <copyright file="StagingRepository.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Data.Repositories
{
    using System.Data;
    using ATEC_API.Data.DTO.StagingDTO;
    using ATEC_API.Data.IRepositories;
    using ATEC_API.Data.Service;
    using ATEC_API.Data.StoredProcedures;
    using ATEC_API.GeneralModels;
    using ATEC_API.GeneralModels.MESATECModels.StagingResponse;
    using Dapper;
    using Microsoft.Data.SqlClient;

    public class StagingRepository : IStagingRepository
    {
        private readonly IDapperConnection _dapperConnection;
        private readonly DapperModelPagination _dapperModelPagination;

        public StagingRepository(IDapperConnection dapperConnection , DapperModelPagination dapperModelPagination)
        {
            this._dapperConnection = dapperConnection;
            this._dapperModelPagination = dapperModelPagination;
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

        public async Task<(IEnumerable<MagazineHistoryDTO>, PageResultsResponse pageResultsResponse)> DapperPagination(MagazineHistoryInput magazineHistoryInput)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SearchData", magazineHistoryInput.searchValue, DbType.String);
            parameters.Add("@@StageValue", magazineHistoryInput.stageValue, DbType.Int32);
            parameters.Add("@PageSize", magazineHistoryInput.pageSize, DbType.Int32);
            parameters.Add("@CurrentPage", magazineHistoryInput.currentPage, DbType.Int32);

            var magazineDetailList = Enumerable.Empty<MagazineHistoryDTO>();
            var pageResult = new PageResultsResponse();

            (magazineDetailList, pageResult) = await _dapperModelPagination
                                                             .GetDetailsAndPagingInfoAsync<MagazineHistoryDTO>(StagingSP.usp_Magazine_History_Search_API, parameters);

            return (magazineDetailList, pageResult);
        }
    }
}