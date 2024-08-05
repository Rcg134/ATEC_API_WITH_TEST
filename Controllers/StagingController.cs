using System.Text.Json;
using ATEC_API.Data.DTO.StagingDTO;
using ATEC_API.Data.IRepositories;
using ATEC_API.GeneralModels;
using ATEC_API.GeneralModels.MESATECModels.StagingResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ATEC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StagingController : Controller
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
            return Ok(new GeneralResponse
            {
                Details = checkLot
            });
        }


        [HttpGet("GetMaterialCustomer")]
        public async Task<IActionResult> GetMaterialCustomer([FromHeader] int paramMaterialType)
        {
            var getCustomer = await _stagingRepository.GetMaterialCustomer(paramMaterialType);
            return Ok(new GeneralResponse
            {
                Details = getCustomer
            });
        }

        //[HttpPost("GetEpoxyDetails")]
        //public async Task<IActionResult> GetEpoxyDetails([FromBody] MaterialStaging staging)
        //{
        //    var materialStaging = new MaterialStagingDTO
        //    {
        //        Sid = staging.paramSid,
        //        MaterialId = staging.paramMaterialId,
        //        Serial = staging.paramSerial,
        //        ExpirationDate = staging.paramExpirationDate,
        //        CustomerCode = staging.paramCustomerCode,
        //        MaterialType = staging.paramMaterialType
        //    };

        //    var getEpoxyDetails = await _stagingRepository.GetMaterialDetail(materialStaging);

        //    return Ok(new GeneralResponse
        //    {
        //        Details = getEpoxyDetails
        //    });
        //}




    }
}