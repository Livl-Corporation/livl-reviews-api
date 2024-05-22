using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LivlReviewsApi.Attributes;

public class UserIdClaimAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentUser = context.HttpContext.User;
        var userIdClaim = currentUser.Claims.FirstOrDefault(c => c.Type == "userGUID");
        if (userIdClaim == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        context.HttpContext.Items["UserId"] = userIdClaim.Value;
        base.OnActionExecuting(context);
    }
}