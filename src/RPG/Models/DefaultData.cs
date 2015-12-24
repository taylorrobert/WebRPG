using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RPG.Models.SchemaModels;
using RPG.Services;

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

            var corporation = new Corporation()
            {
                Name = "SpaceCo Industries, Inc",
                Cash = 10000,
                PublicInterest = 20,
                RD = 20,
                Readiness = 20,
                Reputation = 20,
                TurnsRemaining = 10,
                BusinessMultiplier = 1.00,
                User = user
            };

            var person1 = new Person()
            {
                Name = "Hans Grosserman",
                Description = "One wily German dude.",
                Position = "Engineer",
                TurnSalary = 100,
                Celebrity = false,
                Business = 5,
                Experience = 5,
                Fitness = 5,
                Intelligence = 5,
                SigningBonus = 1000,
                SeverancePayout = 1000
            };

            var person2 = new Person()
            {
                Name = "Jim Johannsen",
                Description = "A smart businessman.",
                Position = "Financier",
                TurnSalary = 80,
                Celebrity = true,
                Business = 5,
                Experience = 5,
                Fitness = 2,
                Intelligence = 2,
                SigningBonus = 800,
                SeverancePayout = 800

            };

            var person3 = new Person()
            {
                Name = "Li Chen",
                Description = "A guy from China who is really good at putting shit together.",
                Position = "Technician",
                TurnSalary = 80,
                Celebrity = false,
                Business = 0,
                Experience = 7,
                Fitness = 4,
                Intelligence = 2,
                SigningBonus = 1000,
                SeverancePayout = 1000
            };

            var persons = new List<Person> {person1, person2, person3};


            var cp1 = new CorporationPerson()
            {
                Corporation = corporation,
                Hired = false,
                Person = person1
            };

            var cp2 = new CorporationPerson()
            {
                Corporation = corporation,
                Hired = false,
                Person = person2
            };

            var cp3 = new CorporationPerson()
            {
                Corporation = corporation,
                Hired = false,
                Person = person3
            };

            var contract = new Contract()
            {
                Name = "The Road to Damnation",
                Script = File.ReadAllText(@"scripts\contracts\testcontract.txt")
            };

            var corpPersons = new List<CorporationPerson> { cp1, cp2, cp3};

            context.Users.Add(user);
            context.Corporations.Add(corporation);
            persons.ForEach(p => context.Persons.Add(p));
            corpPersons.ForEach(c => context.CorporationPersons.Add(c));
            context.Contracts.Add(contract);

            ResearchTree.CreateTestTreeInDB(context, corporation);

            context.SaveChanges();
        }
    }
}
