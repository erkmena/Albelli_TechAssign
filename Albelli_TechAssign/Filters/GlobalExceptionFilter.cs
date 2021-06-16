using Albelli_TechAssign.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            int statusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.StatusCode = statusCode;
            context.Result = new JsonResult(new ExceptionResponse
            {
                StatusCode = statusCode,
                Message = context.Exception.Message //Can be modified as hidden or static exception message for Public API issues
            });
        }
    }
}
