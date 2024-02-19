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
                    Message = "Validation Error",
                    Payload = null,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Success = false,
                    Errors = context.ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList()
                };
                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
