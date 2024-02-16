using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Assignment_UserEntity.Dtos;

namespace Assignment_UserEntity.Helpers.CustomAuthFilter
{
    public class CustomAuthFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
    {
        public AuthorizationPolicy Policy { get; }

        public CustomAuthFilter()
        {
            Policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Allow Anonymous skips all authorization
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }

            var policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
            var authenticateResult = await policyEvaluator.AuthenticateAsync(Policy, context.HttpContext);
            var authorizeResult = await policyEvaluator.AuthorizeAsync(Policy, authenticateResult, context.HttpContext, context);

            if (authorizeResult.Challenged)
            {
                // Return custom 401 result
                var response = new ResponseDto();
                response.Success = false;
                response.Message = "Unauthorized";
                response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new UnauthorizedObjectResult(response);
            }
        }
    }
}
