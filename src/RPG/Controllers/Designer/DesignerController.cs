using Microsoft.AspNet.Mvc;
using RPG.Models;

namespace RPG.Controllers
{
    public class DesignerController : Controller
    {
        private ApplicationDbContext _context;

        public DesignerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Characters
        public IActionResult Designer()
        {
            return View();
        }
    }
}
