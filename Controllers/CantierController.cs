// <copyright file="CantierController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATEC_API.Controllers
{
    using ATEC_API.Data.DTO.Cantier;
    using ATEC_API.Data.IRepositories;
    using ATEC_API.GeneralModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CantierController(ICantierRepository cantierRepository,
                                   ILogger<CantierController> logger) : Controller
    {
        private readonly ICantierRepository _cantierRepository = cantierRepository;
        private readonly ILogger<CantierController> _logger = logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="paramLotNumber"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet("RecipeLoadDetails")]
        public async Task<IActionResult> RecipeLoadDetails([FromHeader] string paramLotNumber)
        {
            var cantier = new CantierDTO
            {
                LotNumber = paramLotNumber,
            };

            var getLotDetails = await _cantierRepository.RecipeLoadDetails(cantier);

            return this.Ok(new GeneralResponse
            {
                Details = getLotDetails,
            });
        }

        [HttpGet("GetLotDetails")]
        public async Task<IActionResult> GetLotDetails([FromHeader] string paramLotNumber)
        {
            this._logger.LogInformation($"");


            var cantier = new CantierDTO
            {
                LotNumber = paramLotNumber,
            };

            var getLotDetails = await _cantierRepository.GetLotDetails(cantier);

            return this.Ok(new GeneralResponse
            {
                Details = getLotDetails,
            });
        }

        [HttpGet("GetLotDetailsTrackIn")]
        public async Task<IActionResult> GetLotDetailsTrackIn([FromHeader] string paramLotNumber)
        {
            var cantier = new CantierDTO
            {
                LotNumber = paramLotNumber,
            };

            var getTrackInDetails = await _cantierRepository.GetTrackInDetails(cantier);

            return this.Ok(new GeneralResponse
            {
                Details = getTrackInDetails,
            });
        }

        [HttpGet("GetLotDetailsTrackOut")]
        public async Task<IActionResult> GetLotDetailsTrackOut([FromHeader] string paramLotNumber)
        {
            var cantier = new CantierDTO
            {
                LotNumber = paramLotNumber,
            };

            var getTrackInDetails = await _cantierRepository.GetTrackOutDetails(cantier);

            return this.Ok(new GeneralResponse
            {
                Details = getTrackInDetails,
            });
        }
    }
}
