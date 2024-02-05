using Assignment_UserEntity.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Assignment_UserEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            if (value == null)
            {
                return new OkObjectResult(new
                {
                    statusCode = StatusCodes.Status404NotFound,
                    data = value,
                    Message = "Not found"
                });
            }

            return new OkObjectResult(new
            {
                statusCode = StatusCodes.Status200OK,
                data = value,
                message = "Success"
            });
        }

        public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object? value)
        {
            return new BadRequestObjectResult(new
            {
                StatusCode = StatusCodes.Status400BadRequest,
                message = value
            });
        }
    }
}
