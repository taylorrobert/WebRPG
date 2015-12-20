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
                TimeStamp = DateTime.Now
            };
            db.LogMessages.Add(msg);
        }

        public static string FormatMessages(List<LogMessage> messages)
        {
            //Handle formatting the messages for the client side
            var formattedMessages = messages[0].Message;

            foreach (var s in messages)
            {
                if (messages.IndexOf(s) > 0)
                {
                    formattedMessages += @"<br><br>" + s.Message;
                }
            }

            return formattedMessages;
        }

        public static string GetLogs(ApplicationDbContext db, Corporation corp)
        {
            var logs = db.LogMessages.Where(l => l.Corporation.Id == corp.Id).OrderByDescending(l => l.TimeStamp).ToList();
            
            //db.Remove(logs);

            return FormatMessages(logs);
        }
    }
}
