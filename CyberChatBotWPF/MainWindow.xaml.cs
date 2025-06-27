using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CyberChatBotWPF
{
    public partial class MainWindow : Window
    {
        private List<CyberTask> taskList = new();
        private DispatcherTimer reminderTimer;
        private List<string> activityLog = new();
        private const int MaxLogEntries = 10;

        private Dictionary<string, string> keywordResponses = new()
        {
            {"password", "Use strong, unique passwords for each account."},
            {"phishing", "Phishing tricks users into giving sensitive info!"},
            {"malware", "Keep your system and antivirus updated."},
            {"2fa", "Enable Two-Factor Authentication for extra security."},
            {"antivirus", "A reliable antivirus helps block threats."},
            {"scam", "Verify suspicious messages before acting!"},
            {"privacy", "Be careful what personal info you share."},
            {"firewall", "Firewalls protect your network traffic."}
        };

        public MainWindow()
        {
            InitializeComponent();
            reminderTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
            reminderTimer.Tick += CheckReminders;
            reminderTimer.Start();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AddToChat($"You: {input}");
            HandleUserInput(input.ToLower());
            UserInput.Clear();
        }

        private void HandleUserInput(string input)
        {
            if (input.Contains("show activity log") || input.Contains("what have you done"))
            {
                ShowActivityLog();
                return;
            }

            if (Regex.IsMatch(input, @"\b(add|create|set)\b.*\b(task|reminder|to-do)\b"))
            {
                string action = ExtractAction(input);
                var task = new CyberTask { Title = action };
                taskList.Add(task);
                TaskList.Items.Add(task);
                AddToChat($"SecureBot: Task added: '{action}'. Want to set a reminder?");
                LogActivity($"Task added: '{action}'");
                return;
            }

            if (Regex.IsMatch(input, @"\b(remind me to|set a reminder for)\b"))
            {
                string action = ExtractAction(input);
                DateTime reminderTime = DateTime.Now.AddDays(1);
                var task = new CyberTask { Title = action, ReminderTime = reminderTime };
                taskList.Add(task);
                TaskList.Items.Add(task);
                AddToChat($"SecureBot: Reminder set for '{action}' tomorrow.");
                LogActivity($"Reminder set: '{action}' at {reminderTime:g}");
                return;
            }

            if (input.Contains("quiz"))
            {
                AddToChat("SecureBot: Starting the cybersecurity quiz...");
                LogActivity("Quiz started");
                StartQuiz_Click(null, null);
                return;
            }

            foreach (var kv in keywordResponses)
            {
                if (input.Contains(kv.Key))
                {
                    AddToChat($"SecureBot: {kv.Value}");
                    LogActivity($"Keyword response triggered: '{kv.Key}'");
                    return;
                }
            }

            AddToChat("SecureBot: Sorry, I didn't understand. Try another command.");
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitle.Text.Trim();
            string reminderInput = TaskReminder.Text.Trim();
            DateTime? reminderTime = null;

            if (!string.IsNullOrEmpty(reminderInput))
            {
                reminderTime = ParseReminder(reminderInput);
            }

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            var task = new CyberTask { Title = title, ReminderTime = reminderTime };
            taskList.Add(task);
            TaskList.Items.Add(task);
            AddToChat($"SecureBot: Task '{title}' added.");
            LogActivity($"Task added: '{title}'" + (reminderTime.HasValue ? $" (Reminder: {reminderTime:g})" : ""));

            TaskTitle.Text = "Enter Task Title";
            TaskReminder.Text = "e.g., in 3 days";
        }

        private DateTime? ParseReminder(string input)
        {
            Match m = Regex.Match(input.ToLower(), @"in\s+(\d+)\s+(day|hour|minute)s?");
            if (!m.Success) return null;

            int val = int.Parse(m.Groups[1].Value);
            return m.Groups[2].Value.StartsWith("day") ? DateTime.Now.AddDays(val)
                 : m.Groups[2].Value.StartsWith("hour") ? DateTime.Now.AddHours(val)
                 : DateTime.Now.AddMinutes(val);
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is CyberTask t)
            {
                taskList.Remove(t);
                TaskList.Items.Remove(t);
                AddToChat($"SecureBot: Completed '{t.Title}'.");
                LogActivity($"Task completed: '{t.Title}'");
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is CyberTask t)
            {
                taskList.Remove(t);
                TaskList.Items.Remove(t);
                AddToChat($"SecureBot: Deleted '{t.Title}'.");
                LogActivity($"Task deleted: '{t.Title}'");
            }
        }

        private void CheckReminders(object? sender, EventArgs e)
        {
            foreach (var task in taskList)
            {
                if (task.ReminderTime.HasValue && task.ReminderTime.Value <= DateTime.Now)
                {
                    MessageBox.Show($"⏰ Reminder:\n{task.Title}", "Reminder");
                    LogActivity($"Reminder triggered for: '{task.Title}'");
                    task.ReminderTime = null;
                }
            }
        }

        private void ShowActivityLog_Click(object sender, RoutedEventArgs e)
        {
            ShowActivityLog();
        }

        private void ShowActivityLog()
        {
            if (activityLog.Count == 0)
            {
                AddToChat("SecureBot: No recent activity available.");
                return;
            }

            AddToChat("SecureBot: Recent activity:");
            foreach (string entry in activityLog)
                AddToChat($"• {entry}");
        }

        private string ExtractAction(string input)
        {
            string clean = Regex.Replace(input,
                @"(add|create|set)\s+(a\s+)?(task|reminder|to-do)\s*(to)?\s*",
                "", RegexOptions.IgnoreCase).Trim();
            return string.IsNullOrEmpty(clean) ? "New Task" :
                   char.ToUpper(clean[0]) + clean.Substring(1);
        }

        private void StartQuiz_Click(object? sender, RoutedEventArgs? e)
        {
            Hide();
            QuizWindow quiz = new();
            quiz.ShowDialog();
            Show();
        }

        private void LogActivity(string action)
        {
            string entry = $"{DateTime.Now:g}: {action}";
            activityLog.Add(entry);
            if (activityLog.Count > MaxLogEntries)
                activityLog.RemoveAt(0);
        }

        private void AddToChat(string message)
        {
            ChatHistory.Text += $"{message}\n";
        }

        private void ClearTextOnFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb) tb.Clear();
        }
    }
}
