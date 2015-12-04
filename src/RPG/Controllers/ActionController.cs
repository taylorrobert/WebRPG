using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using RPG.Models;
using RPG.Services;
using RPG.ViewModels.Action;

namespace RPG.Controllers
{
    public class ActionController : BaseController
    {
        public ActionController(ApplicationDbContext context) : base(context)
        {
            
        }

        // GET: Action
        public IActionResult Index()
        {
            if (!User.IsSignedIn()) return RedirectToAction("Login", "Account");

            if (string.IsNullOrEmpty(_applicationUser.ActiveCharacter.PublicId)) return RedirectToAction("Index", "Characters");

            var characters = DataService.GetCharactersByUser(_context, User.GetUserName().ToUpper());
            if (!characters.Select(c => c.PublicId).ToList().Contains(_applicationUser.ActiveCharacter.PublicId)) return RedirectToAction("Index", "Characters");

            var actionModel = new ActionModel();
            actionModel.Character = _applicationUser.ActiveCharacter;

            if (_applicationUser.ActiveCharacter.Region == null && _applicationUser.ActiveCharacter.Location == null)
            {
                //Character has never logged in before
                SchemaService.ConfigureCharacterFirstLogin(_context, _applicationUser.ActiveCharacter, actionModel);
                _context.SaveChanges();
            }
            
            return View(actionModel);
        }

        public IActionResult CharacterSelect()
        {
            var characters = DataService.GetCharactersByUser(_context, User.GetUserName().ToUpper());
            return View(characters);
        }
    }
}
