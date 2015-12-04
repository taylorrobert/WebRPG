using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Models;

namespace RPG.Services
{
    public class DataService
    {
        public static List<Character> GetCharactersByUser(ApplicationDbContext context, string user)
        {
            return context.Characters.Where(c => c.User.UserName.ToUpper() == user.ToUpper()).ToList();
        } 
    }
}
