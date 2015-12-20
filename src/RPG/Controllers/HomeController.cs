using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RPG.Models;

namespace RPG.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ApplicationDbContext context) : base(context)
        {
            
        }

        public IActionResult Index()
        {
            if (User.IsSignedIn()) return RedirectToAction("Index", "Action");
            return View();
        }
    }
}
