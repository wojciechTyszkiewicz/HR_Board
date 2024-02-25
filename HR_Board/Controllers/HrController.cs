using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_Board.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrController : ControllerBase
    {
        private readonly string controller;
        private readonly LinkGenerator linkGenerator;

        public HrController(string controller, LinkGenerator linkGenerator)
        {
            this.controller = controller;
            this.linkGenerator = linkGenerator;
        }

        protected IResult CreatedAtGet(string action, object id)
        {
            var location = linkGenerator.GetPathByAction(
                httpContext: this.HttpContext,
                action: action,
                controller: this.controller,
                values: new { id = id });
            return Results.Created(location, id);
        }
    }
}
