using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using RPG.Models;
using RPG.Models.SchemaModels;
using RPG.Services;
using ActionModel = RPG.Models.ActionModel;

namespace RPG.Controllers
{
    [Authorize]
    public class ActionController : BaseController
    {
        private DataCache data;

        public ActionController(ApplicationDbContext context) : base(context)
        {
            
        }

        // GET: Action
        public IActionResult Index(ActionModel actionModel)
        {
            var model = ActionModel.NewActionModel(db, User.Identity.Name);

            data = new DataCache(db, model.User, model.Corporation);
            data.RefreshCache();
            model.DataCache = data;
            

            return View(model);
        }

        public IActionResult EndTurn(ClientActionModel clientModel)
        {
            var newModel = ActionModel.NewActionModel(db, User.Identity.Name);
            newModel.DataCache = data;

            var response = ActionService.PerformActions(db, clientModel, data);
            return Json(newModel);
        }
    }
}
