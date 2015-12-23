using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Models;

namespace RPG.Services
{
    public static class LogService
    {
        public static void Log(ApplicationDbContext db, Corporation corporation, string message)
        {
            var msg = new LogMessage()
            {
                Corporation = corporation,
                Message = message,
                TimeStamp = DateTime.Now,
                TurnCount = corporation.TurnCount
            };
            db.LogMessages.Add(msg);
        }

        public static string FormatMessages(List<LogMessage> messages)
        {
            //Handle formatting the messages for the client side
            if (!messages.Any()) return "";
            var formattedMessages = @"<a href='#' class='list-group-item'><i class='fa fa-fw fa-check'></i>" + messages[0].Message + "</a>"; 

            foreach (var s in messages)
            {
                if (messages.IndexOf(s) > 0)
                {
                    formattedMessages +=
                        @"<a href='#' class='list-group-item'><i class='fa fa-fw fa-check'></i>"+ s.Message +"</a>";

                }
            }

            return formattedMessages;
        }

        public static string GetLogs(ApplicationDbContext db, Corporation corp)
        {
            var logs = db.LogMessages.Where(l => l.Corporation.Id == corp.Id).OrderByDescending(l => l.TimeStamp).ToList();

            return FormatMessages(logs);
        }

        public static string GetLogsByTurn(ApplicationDbContext db, Corporation corp, long turnNumber)
        {
            var turns = db.LogMessages.Where(l => l.Corporation.Id == corp.Id && l.TurnCount == turnNumber).ToList();
            return FormatMessages(turns);
        }
    }
}
