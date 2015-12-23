using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using RPG.Models.SchemaModels;

namespace RPG.Models
{
    public class ActionModel
    {
        public ActionModel()
        {
            ClientModel = new ClientActionModel();
            Actions = "";
            Messages = "";
        }

        public int Id { get; set; }
        public Corporation Corporation { get; set; }
        public ApplicationUser User { get; set; }
        public string Test { get; set; }
        public ClientActionModel ClientModel { get; set; }
        public string Messages { get; set; }
        
        public DataCache DataCache { get; set; }
        public string ResearchTreeRocketryBody { get; set; }
        public string Actions { get; set; }




        public static ActionModel NewActionModel(ApplicationDbContext db, string identityName)
        {
            var model = new ActionModel();

            model.User = db.Users.FirstOrDefault(u => u.UserName == identityName);
            model.Corporation = db.Corporations.FirstOrDefault(c => c.User.Id == model.User.Id);
            model.Test = "TESTINGYAY";

            model.DataCache = new DataCache(db, model.User, model.Corporation);

            return model;
        }

        public void BuildViewItems(ApplicationDbContext db)
        {
            ResearchTreeRocketryBody = ResearchTree.GetTreeBody(ResearchTree.BuildTreeForView(this, db), db, this);
        }
    }
}
