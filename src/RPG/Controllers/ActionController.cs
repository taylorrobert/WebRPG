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

            var currentCharacterPublicId = HttpContext.Request.Cookies["CURRENT_CHARACTER_PUBLIC_ID"];

            if (string.IsNullOrEmpty(currentCharacterPublicId)) return RedirectToAction("Index", "Characters");

            var characters = DataService.GetCharactersByUser(_context, User.GetUserName().ToUpper());
            if (!characters.Select(c => c.PublicId).ToList().Contains(currentCharacterPublicId.ToString())) return RedirectToAction("Index", "Characters");

            var actionModel = new ActionModel();
            actionModel.Character = characters.FirstOrDefault(c => c.PublicId == currentCharacterPublicId.ToString());
            
            return View(actionModel);
        }

        public IActionResult CharacterSelect()
        {
            var characters = DataService.GetCharactersByUser(_context, User.GetUserName().ToUpper());
            return View(characters);
        }
    }
}
