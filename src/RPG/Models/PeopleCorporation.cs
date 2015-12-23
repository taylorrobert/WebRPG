using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Models
{
    public class CorporationPerson
    {
        public int Id { get; set; }
        public Corporation Corporation { get; set; }
        public Person Person { get; set; }

        [DefaultValue(false)]
        public bool Hired { get; set; }

        public static List<CorporationPerson> GetPeopleByHiredStatus(bool hired, IEnumerable<CorporationPerson> cps)
        {
            return cps.Where(c => c.Hired == hired).ToList();
        }
    }
}
