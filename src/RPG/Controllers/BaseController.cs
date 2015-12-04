using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using RPG.Models;

namespace RPG.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationDbContext _context;
        public ApplicationUser _applicationUser;

        public BaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _applicationUser = _context.Users.FirstOrDefault(u => u.Id == User.GetUserId());
            base.OnActionExecuting(context);
        }
    }
}
