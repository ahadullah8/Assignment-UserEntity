using Assignment_UserEntity.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Assignment_UserEntity.Helpers.Validator
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context==null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            if (!context.ModelState.IsValid)
            {
                var response = new ResponseDto()
                {
                    Message = "Error",
                    Payload = "One or more validations failed",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Success = false
                };
                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
