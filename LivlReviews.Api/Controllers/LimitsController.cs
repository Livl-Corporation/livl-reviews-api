using Microsoft.AspNetCore.Mvc;

namespace LivlReviews.Api.Controllers;

public class LimitsController : ControllerBase
{
    [HttpPost("set")]
    public ActionResult SetLimit()
    {
        return Ok();
    }
}