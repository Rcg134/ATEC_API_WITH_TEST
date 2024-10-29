// <copyright file="ValidateModelAttribute.cs" company="ATEC">
// Copyright (c) ATEC. All rights reserved.
// </copyright>

namespace ATEC_API.Filters
{
    using System.Net;
    using System.Text.Json;
    using ATEC_API.Controllers;
    using ATEC_API.GeneralModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private readonly ILogger<StagingController> _logger;

        public ValidateModelAttribute(ILogger<StagingController> logger)
        {
            _logger = logger;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                                    .SelectMany(modelStateEntry => modelStateEntry.Value.Errors.Select(error => new
                                    {
                                        Key = modelStateEntry.Key,
                                        Message = error.ErrorMessage
                                    }))
                                    .GroupBy(x => x.Key, x => x.Message)
                                    .ToDictionary(g => g.Key, g => g.ToArray());

                var errorResponse = new ErrorResponse
                {
                    status = (int)HttpStatusCode.BadRequest,
                    error = errors
                };

                _logger.LogError($"Error has been occured with details of {JsonSerializer.Serialize(errorResponse)}");

                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }
    }
}