using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RPG.Models
{
    public static class DefaultData
    {
        public static void InitializeIfFreshDB(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            //If this table is empty, we know the DB is fresh
            if (context.Dev.FirstOrDefault() != null) return;

            context.Dev.Add(new Dev
            {
                ProductName = "Text RPG Adventure",
                Version = "0.0.1",
                Author = "Taylor"
            });

            var user = new ApplicationUser()
            {
                Id = "a312887d-6242-4537-b7b2-3c6d6473c04d",
                AccessFailedCount = 0,
                ConcurrencyStamp = "645355e8-cade-4815-9219-36a792c80461",
                Email = "taylorhill@gmail.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                LockoutEnd = null,
                NormalizedEmail = "TAYLORHILL@GMAIL.COM",
                NormalizedUserName = "TAYLORHILL@GMAIL.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEEv6pe6NxpSgbuOQ9k3zrGYqUo6fHzxM6knslraP71+QxfeJRJjD4bvW/FVRY5bQ7w==",
                PhoneNumber = null,
                SecurityStamp = "0e88fa93-e579-4354-9e20-7d0f0cd36b30",
                TwoFactorEnabled = false,
                UserName = "taylorhill@gmail.com"
            };

            context.Users.Add(user);

            var character = new Character
            {
                Level = 1,
                Name = "Agamemnon",
                User = user
            };
            
            context.Characters.Add(character);


            user.ActiveCharacter = character;

            context.SaveChanges();
        }
    }
}
