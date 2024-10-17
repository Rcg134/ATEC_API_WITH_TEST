// <copyright file="StagingController.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Controllers
{
    using System.Text.Json;
    using ATEC_API.Data.DTO.StagingDTO;
    using ATEC_API.Data.IRepositories;
    using ATEC_API.GeneralModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StagingController : ControllerBase
    {
        private readonly IStagingRepository _stagingRepository;
        private readonly ILogger<StagingController> _logger;

        public StagingController(IStagingRepository stagingRepository ,
                                 ILogger<StagingController> logger)
        {
            _logger = logger;
            _stagingRepository = stagingRepository;
        }

        [HttpGet("IsTrackOut")]
        public async Task<IActionResult> IsLotTrackOut([FromHeader] string paramLotAlias)
        {
            _logger.LogInformation($"Invoking IsLotTrackOut method with {paramLotAlias} parameters");

            var staging = new StagingDTO
            {
                LotAlias = paramLotAlias,
            };

            var isTrackOut = await _stagingRepository.IsTrackOut(staging);


            _logger.LogInformation($"{paramLotAlias} details are {JsonSerializer.Serialize(isTrackOut)}");

            return Ok(new GeneralResponse
            {
                Details = isTrackOut,
            });
        }

        [HttpGet("GetEpoxyDetails")]
        public async Task<IActionResult> GetEpoxyDetails([FromHeader] string paramSid,
                                                         [FromHeader] string paramMaterialId,
                                                         [FromHeader] string paramSerial,
                                                         [FromHeader] string paramExpirationDate,
                                                         [FromHeader] int paramCustomerCode,
                                                         [FromHeader] int paramMaterialType,
                                                         [FromHeader] int paramUserCode)
        {
            _logger.LogInformation($"Invoking GetEpoxyDetails method");

            var materialStaging = new MaterialStagingDTO
            {
                Sid = paramSid,
                MaterialId = paramMaterialId,
                Serial = paramSerial,
                ExpirationDate = paramExpirationDate,
                CustomerCode = paramCustomerCode,
                MaterialType = paramMaterialType,
                Usercode = paramUserCode
            };

            var getEpoxyDetails = await _stagingRepository.GetMaterialDetail(materialStaging);

            _logger.LogInformation($"Results {JsonSerializer.Serialize(getEpoxyDetails)}");

            return Ok(new GeneralResponse
            {
                Details = getEpoxyDetails
            });
        }

        [HttpGet("CheckLotNumber")]
        public async Task<IActionResult> CheckParam([FromHeader] string paramLotNumber,
                                                    [FromHeader] string? paramMachineNo,       
                                                    [FromHeader] int paramCustomerCode,
                                                    [FromHeader] int paramMode,
                                                    [FromHeader] string paramSID,
                                                    [FromHeader] string paramMaterialId,
                                                    [FromHeader] string paramSerial)
        {
            _logger.LogInformation($"Invoking CheckParam method");

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

            _logger.LogInformation($"Results {JsonSerializer.Serialize(checkLot)}");

            return Ok(new GeneralResponse
            {
                Details = checkLot
            });
        }


        [HttpGet("GetMaterialCustomer")]
        public async Task<IActionResult> GetMaterialCustomer([FromHeader] int paramMaterialType)
        {
            _logger.LogInformation($"Invoking GetMaterialCustomer method");

            var getCustomer = await _stagingRepository.GetMaterialCustomer(paramMaterialType);

            _logger.LogInformation($"Results {JsonSerializer.Serialize(getCustomer)}");

            return Ok(new GeneralResponse
            {
                Details = getCustomer
            });
        }

        [HttpGet("GetMaterialHistory")]
        public async Task<IActionResult> GetMaterialHistory([FromHeader] int paramMaterialType,
                                                            [FromHeader] int paramCustomerCode,
                                                            [FromHeader] DateTime? paramDateFrom,
                                                            [FromHeader] DateTime? paramDateTo,
                                                            [FromHeader] int paramMode)
        {
            _logger.LogInformation($"Invoking GetMaterialHistory method");

            var materialHistory = new MaterialStagingHistoryDTO
            {
                MaterialType = paramMaterialType,
                CustomerCode = paramCustomerCode,
                DateFrom = paramDateFrom,
                DateTo = paramDateTo,
                Mode = paramMode
            };


            if (paramMode == 1) {
                var getCustomerHistory = await _stagingRepository.GetCustomerHistory(materialHistory);

                _logger.LogInformation($"Results {JsonSerializer.Serialize(getCustomerHistory)}");

                return Ok(new GeneralResponse
                {
                    Details = getCustomerHistory
                });

              
            }

            var getMaterialHistory = await _stagingRepository.GetMaterialHistory(materialHistory);

            _logger.LogInformation($"Results {JsonSerializer.Serialize(getMaterialHistory)}");

            return Ok(new GeneralResponse
            {
                Details = getMaterialHistory
            });
        }

    }
}