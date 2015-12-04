using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Lib;
using RPG.Lib.Schema;
using RPG.Models;
using RPG.ViewModels.Action;

namespace RPG.Services
{
    public class DataService
    {
        public static List<Character> GetCharactersByUser(ApplicationDbContext context, string user)
        {
            return context.Characters.Where(c => c.User.UserName.ToUpper() == user.ToUpper()).ToList();
        }

        public static Character GetCharacterStatus(ApplicationDbContext context, string publicId)
        {
            return context.Characters.FirstOrDefault(c => c.PublicId == publicId);
        }

        public static ActionModel GetInitialState(string userId, ApplicationDbContext context)
        {
            var mcm = new ActionModel();

            var firstOrDefault = context.Characters.FirstOrDefault(c => c.User.Id == userId);


            return null;
        }

        public static SchemaRegion GetStartingRegion(ApplicationDbContext context)
        {
            return context.SchemaRegions.FirstOrDefault(r => r.ReferenceName == Constants.StartingRegionName);
        }

        public static SchemaLocation GetStartingLocation(ApplicationDbContext context)
        {
            return context.SchemaLocations.FirstOrDefault(r => r.ReferenceName == Constants.StartingLocationName);
        }

        public static SchemaQuest GetStartingQuest(ApplicationDbContext context)
        {
            return context.SchemaQuests.FirstOrDefault(q => q.ReferenceName == Constants.StartingQuestName);
        }
    }
}
