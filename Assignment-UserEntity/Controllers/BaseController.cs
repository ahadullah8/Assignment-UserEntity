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
            response.Message = "Success";
            response.Payload = value;
            response.Success = true;
            return new OkObjectResult(response);
        }

        public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object? value)
        {
            ResponseDto response = new();
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Message = value.ToString();
            response.Success = false;
            return new BadRequestObjectResult(response);
        }
    }
}
