using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Routing;
using RPG.Models;

namespace RPG.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationDbContext db;
        public ApplicationUser _applicationUser;
        public Corporation _corporation;

        public BaseController(ApplicationDbContext context)
        {
            db = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _applicationUser = db.Users.FirstOrDefault(u => u.Id == User.GetUserId());

            if (_applicationUser != null)
            {
                _corporation = db.Corporations.FirstOrDefault(c => c.User.Id == _applicationUser.Id);
                if (_corporation == null)
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"Controller", "Corporations"},
                            {"Action", "Create"}
                        });
            }
            base.OnActionExecuting(context);
        }
    }
}
