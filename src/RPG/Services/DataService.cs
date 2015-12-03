using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using RPG.Models;

namespace RPG.Services
{
    public static class DataService
    {
        public static Character GetCharacterStatus(ApplicationDbContext context, string publicId)
        {
            return context.Characters.FirstOrDefault(c => c.PublicId == publicId);
        }

        public static MasterCharacterModel GetInitialState(string userId, ApplicationDbContext context)
        {
            var mcm = new MasterCharacterModel();

            var firstOrDefault = context.Characters.FirstOrDefault(c => c.User.Id == userId);


            return null;
        }
    }
}
