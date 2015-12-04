using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using RPG.Models;
using RPG.Services;

namespace RPG.Controllers
{
    public class CharactersController : BaseController
    {
        public CharactersController(ApplicationDbContext context) : base(context)
        {
            
        }

        // GET: Characters
        public IActionResult Index()
        {
            return View(_context.Characters.ToList());
        }

        // GET: Characters/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Character character = _context.Characters.Single(m => m.Id == id);
            if (character == null)
            {
                return HttpNotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Characters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Character character)
        {
            if (ModelState.IsValid)
            {
                character.User = _applicationUser;
                character.Location = ActionService.GetStartingLocation(_context);
                character.Region = ActionService.GetStartingRegion(_context);

                _context.Characters.Add(character);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(character);
        }

        // GET: Characters/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Character character = _context.Characters.Single(m => m.Id == id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Characters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Character character)
        {
            if (ModelState.IsValid)
            {
                _context.Update(character);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(character);
        }

        // GET: Characters/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Character character = _context.Characters.Single(m => m.Id == id);
            if (character == null)
            {
                return HttpNotFound();
            }

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Character character = _context.Characters.Single(m => m.Id == id);
            _context.Characters.Remove(character);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
