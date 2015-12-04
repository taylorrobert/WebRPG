using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using RPG.Lib;
using RPG.Lib.Schema;
using RPG.Models;
using RPG.ViewModels.Action;

namespace RPG.Services
{
    public static class ActionService
    {
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


    }
}
