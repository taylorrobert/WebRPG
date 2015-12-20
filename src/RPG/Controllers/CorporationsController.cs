using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using RPG.Models;

namespace RPG.Controllers
{
    public class CorporationsController : BaseController
    {

        public CorporationsController(ApplicationDbContext db) : base(db)
        {
              
        }

        // GET: Corporations
        public IActionResult Index()
        {
            return View(db.Corporations.ToList());
        }

        // GET: Corporations/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Corporation corporation = db.Corporations.Single(m => m.Id == id);
            if (corporation == null)
            {
                return HttpNotFound();
            }

            return View(corporation);
        }

        // GET: Corporations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Corporations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(Corporation corporation)
        {
            corporation.User = _applicationUser; 
            if (ModelState.IsValid)
            {
                db.Corporations.Add(corporation);
                db.SaveChanges();
                return RedirectToAction("Index", "Action");
            }
            return View(corporation);
        }

        // GET: Corporations/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Corporation corporation = db.Corporations.Single(m => m.Id == id);
            if (corporation == null)
            {
                return HttpNotFound();
            }
            return View(corporation);
        }

        // POST: Corporations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Corporation corporation)
        {
            if (ModelState.IsValid)
            {
                db.Update(corporation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(corporation);
        }

        // GET: Corporations/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Corporation corporation = db.Corporations.Single(m => m.Id == id);
            if (corporation == null)
            {
                return HttpNotFound();
            }

            return View(corporation);
        }

        // POST: Corporations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Corporation corporation = db.Corporations.Single(m => m.Id == id);
            db.Corporations.Remove(corporation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
