using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Models
{
    public class LogMessage
    {
        public int Id { get; set; }
        public Corporation Corporation { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public long TurnCount { get; set; }
    }
}
