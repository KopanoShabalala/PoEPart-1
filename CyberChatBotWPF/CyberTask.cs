using System;

namespace CyberChatBotWPF
{
    public class CyberTask
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime? ReminderTime { get; set; }

        public override string ToString()
        {
            return $"{Title}" + (ReminderTime.HasValue ? $" (Remind at {ReminderTime:g})" : "");
        }
    }
}
