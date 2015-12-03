using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RPG.Models;
using RPG.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RPG.Controllers
{
    public class ActionController : Controller
    {
        private ApplicationDbContext _context;

        // GET: /<controller>/
        public IActionResult Index()
        {
            var state = DataService.GetInitialState(User.GetUserId(), _context);
            return View();
        }
    }
}
