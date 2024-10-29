// <copyright file="StagingController.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Controllers
{
    using System.Data;
    using System.Text.Json;
    using ATEC_API.Data.DTO.DownloadCompressDTO;
    using ATEC_API.Data.DTO.StagingDTO;
    using ATEC_API.Data.IRepositories;
    using ATEC_API.Data.StoredProcedures;
    using ATEC_API.GeneralModels;
    using Dapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StagingController : ControllerBase
    {
        private readonly IStagingRepository _stagingRepository;
        private readonly ILogger<StagingController> _logger;
        private readonly IDownloadRepository _downloadRepository;

        public StagingController(IStagingRepository stagingRepository,
                                 ILogger<StagingController> logger,
                                 IDownloadRepository downloadRepository)
        {
            this._logger = logger;
            this._downloadRepository = downloadRepository;
            this._stagingRepository = stagingRepository;
        }

        [HttpGet("IsTrackOut")]
        public async Task<IActionResult> IsLotTrackOut([FromHeader] string paramLotAlias)
        {
            this._logger.LogInformation($"Invoking IsLotTrackOut method with {paramLotAlias} parameters");

            var staging = new StagingDTO
            {
                LotAlias = paramLotAlias,
            };

            var isTrackOut = await _stagingRepository.IsTrackOut(staging);

            this._logger.LogInformation($"{paramLotAlias} details are {JsonSerializer.Serialize(isTrackOut)}");

            return this.Ok(new GeneralResponse
            {
                Details = isTrackOut,
            });
        }

        [HttpGet("GetEpoxyDetails")]
        public async Task<IActionResult> GetEpoxyDetails(
            [FromHeader] string paramSid,
            [FromHeader] string paramMaterialId,
            [FromHeader] string paramSerial,
            [FromHeader] string paramExpirationDate,
            [FromHeader] int paramCustomerCode,
            [FromHeader] int paramMaterialType,
            [FromHeader] int paramUserCode)
        {
            this._logger.LogInformation($"Invoking GetEpoxyDetails method");

            var materialStaging = new MaterialStagingDTO
            {
                Sid = paramSid,
                MaterialId = paramMaterialId,
                Serial = paramSerial,
                ExpirationDate = paramExpirationDate,
                CustomerCode = paramCustomerCode,
                MaterialType = paramMaterialType,
                Usercode = paramUserCode,
            };

            var getEpoxyDetails = await _stagingRepository.GetMaterialDetail(materialStaging);

            this._logger.LogInformation($"Results {JsonSerializer.Serialize(getEpoxyDetails)}");

            return this.Ok(new GeneralResponse
            {
                Details = getEpoxyDetails,
            });
        }

        [HttpGet("CheckLotNumber")]
        public async Task<IActionResult> CheckParam(
            [FromHeader] string paramLotNumber,
            [FromHeader] string? paramMachineNo,
            [FromHeader] int paramCustomerCode,
            [FromHeader] int paramMode,
            [FromHeader] string paramSID,
            [FromHeader] string paramMaterialId,
            [FromHeader] string paramSerial)
        {
            this._logger.LogInformation($"Invoking CheckParam method");

            var materialStaging = new MaterialStagingCheckParamDTO
            {
                LotNumber = paramLotNumber,
                Machine = paramMachineNo,
                CustomerCode = paramCustomerCode,
                Mode = paramMode,
                SID = paramSID,
                MaterialId = paramMaterialId,
                Serial = paramSerial
            };

            var checkLot = await _stagingRepository.CheckLotNumber(materialStaging);

            this._logger.LogInformation($"Results {JsonSerializer.Serialize(checkLot)}");

            return this.Ok(new GeneralResponse
            {
                Details = checkLot
            });
        }

        [HttpGet("GetMaterialCustomer")]
        public async Task<IActionResult> GetMaterialCustomer([FromHeader] int paramMaterialType)
        {
            this._logger.LogInformation($"Invoking GetMaterialCustomer method");

            var getCustomer = await _stagingRepository.GetMaterialCustomer(paramMaterialType);

            this._logger.LogInformation($"Results {JsonSerializer.Serialize(getCustomer)}");

            return this.Ok(new GeneralResponse
            {
                Details = getCustomer,
            });
        }

        [HttpGet("GetMaterialHistory")]
        public async Task<IActionResult> GetMaterialHistory(
            [FromHeader] int paramMaterialType,
            [FromHeader] int paramCustomerCode,
            [FromHeader] DateTime? paramDateFrom,
            [FromHeader] DateTime? paramDateTo,
            [FromHeader] int paramMode)
        {
            this._logger.LogInformation($"Invoking GetMaterialHistory method");

            var materialHistory = new MaterialStagingHistoryDTO
            {
                MaterialType = paramMaterialType,
                CustomerCode = paramCustomerCode,
                DateFrom = paramDateFrom,
                DateTo = paramDateTo,
                Mode = paramMode,
            };

            if (paramMode == 1)
            {
                var getCustomerHistory = await _stagingRepository.GetCustomerHistory(materialHistory);

                this._logger.LogInformation($"Results {JsonSerializer.Serialize(getCustomerHistory)}");

                return this.Ok(new GeneralResponse
                {
                    Details = getCustomerHistory,
                });
            }

            var getMaterialHistory = await _stagingRepository.GetMaterialHistory(materialHistory);

            this._logger.LogInformation($"Results {JsonSerializer.Serialize(getMaterialHistory)}");

            return this.Ok(new GeneralResponse
            {
                Details = getMaterialHistory,
            });
        }

        [HttpGet("GetHistoryDetails")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHistoryDetails([FromQuery]MagazineHistoryInput magazineHistoryInput)
        {
            this._logger.LogInformation("GetHistoryDetails method is invoking");

            var magazineDetailList = Enumerable.Empty<MagazineHistoryDTO>();
            var pageResult = new PageResultsResponse();

            (magazineDetailList, pageResult) = await this._stagingRepository.MagazineListDapperPagination(magazineHistoryInput);

            this._logger.LogInformation($"Details {JsonSerializer.Serialize(magazineDetailList)}");

            return this.Ok(new GeneralResponse
            {
                Details = magazineDetailList,
                Response = pageResult,
            });
        }


        [HttpGet("DownloadHistoryList")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadHistoryList(
            [FromQuery] string zipName,
            [FromQuery] string sheetName,
            [FromQuery] MagazineHistoryInput magazineHistoryInput)
        {
            this._logger.LogInformation("DownloadHistoryList method is invoking");

            var downloadParams = new DownloadCompressDTO
            {
                ZipName = zipName,
                SheetName = sheetName,
                SP = StagingSP.usp_Magazine_History_Search_Download_API,
                CacheKey = StagingSP.usp_Magazine_History_Search_Download_API,
            };

            var parametersSP = new DynamicParameters();
            parametersSP.Add("@SearchData", magazineHistoryInput.searchValue, DbType.String);
            parametersSP.Add("@StageValue", magazineHistoryInput.stageValue, DbType.Int32);
            parametersSP.Add("@CustomerCode", magazineHistoryInput.customerCode, DbType.Int32);
            parametersSP.Add("@DateFrom", magazineHistoryInput.dateFrom, DbType.DateTime);
            parametersSP.Add("@DateTo", magazineHistoryInput.dateTo, DbType.DateTime);

            var blob = await this._downloadRepository.DownloadToExcelAndCompress<MagazineHistoryDTO>(downloadParams, parametersSP);

            this._logger.LogInformation($"Details {JsonSerializer.Serialize(downloadParams)}");

            return this.File(blob, "application/zip", downloadParams.ZipName);
        }
    }
}