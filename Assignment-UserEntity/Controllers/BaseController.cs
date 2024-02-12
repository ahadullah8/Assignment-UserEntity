using Assignment_UserEntity.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Resources;

namespace Assignment_UserEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            ResponseDto response = new();
            response.StatusCode = StatusCodes.Status200OK;
            response.Message = "Request completed successfully";
            response.Payload = value;
            response.Success = true;
            return new OkObjectResult(response);
        }
        /// <summary>
        /// Creates an <see cref="OkObjectResult"/> object that produces an <see cref="StatusCodes.Status200OK"/> response.
        /// </summary>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <param name="message">The message to send back to the user as response message.</param>
        /// <returns>The created <see cref="OkObjectResult"/> for the response.</returns>
        protected OkObjectResult Ok([ActionResultObjectValue] object? value, string? message)
        {
            ResponseDto response = new();
            response.StatusCode = StatusCodes.Status200OK;
            response.Message = message;
            response.Payload = value;
            response.Success = true;
            return new OkObjectResult(response);
        }

        public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object? value)
        {
            ResponseDto response = new();
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Payload = value;
            response.Success = false;
            return new BadRequestObjectResult(response);
        }
    }
}
