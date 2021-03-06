﻿using System.Collections.Generic;
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
        public ActionController(ApplicationDbContext context) : base(context)
        {
            
        }

        // GET: Action
        public IActionResult Index(ActionModel actionModel)
        {
            var model = ActionModel.NewActionModel(db, User.Identity.Name);

            var data = new DataCache(db, model.User, model.Corporation);
            data.RefreshCache();
            model.DataCache = data;

            if (model.Corporation.TurnCount > 1)
            {
                model.Messages = LogService.GetLogsByTurn(db, model.Corporation, model.Corporation.TurnCount - 1);
            }

            data.RefreshCache();
            model.BuildViewItems(db);

            return View(model);
        }

        public IActionResult EndTurn(ClientActionModel clientModel)
        {
            var newModel = ActionModel.NewActionModel(db, User.Identity.Name);
            var data = new DataCache(db, newModel.User, newModel.Corporation);
            newModel.DataCache = data;

            ActionService.PerformActions(db, clientModel, data);
            ActionService.ResolveState(db, clientModel, data);

            data.RefreshCache();

            newModel.BuildViewItems(db);

            return Json(newModel);
        }
    }
}
