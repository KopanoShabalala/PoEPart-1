using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CyberChatBotWPF
{
    public partial class QuizWindow : Window
    {
        private List<QuizQuestion> questions = new List<QuizQuestion>();
        private int currentIndex = 0;
        private int score = 0;
        private bool questionAnswered = false;

        public QuizWindow()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayQuestion();
        }

        private void LoadQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion { Question = "What should you do if you receive an email asking for your password?", Options = new[] { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" }, CorrectOptionIndex = 2, Explanation = "Correct! Reporting phishing emails helps prevent scams." },
                new QuizQuestion { Question = "True or False: Using the same password for all accounts is safe.", Options = new[] { "True", "False" }, CorrectOptionIndex = 1, Explanation = "False. Reusing passwords increases the risk if one account is compromised." },
                new QuizQuestion { Question = "Which of the following is a strong password?", Options = new[] { "123456", "Password123", "Summer2020", "Gx!7$pL9@q" }, CorrectOptionIndex = 3, Explanation = "Correct! Strong passwords use symbols, numbers, and no personal info." },
                new QuizQuestion { Question = "What is phishing?", Options = new[] { "A type of malware", "A hacking tool", "A scam to steal personal info", "A secure login method" }, CorrectOptionIndex = 2, Explanation = "Phishing tricks users into giving sensitive information." },
                new QuizQuestion { Question = "True or False: Public Wi-Fi is always secure.", Options = new[] { "True", "False" }, CorrectOptionIndex = 1, Explanation = "False. Public Wi-Fi can be monitored by attackers." },
                new QuizQuestion { Question = "What does 2FA stand for?", Options = new[] { "Two-Factor Authentication", "Two-Firewall Access", "Twice Fast Access", "Two-Factor Attack" }, CorrectOptionIndex = 0, Explanation = "Correct! 2FA adds an extra layer of protection to logins." },
                new QuizQuestion { Question = "Why should software updates be installed promptly?", Options = new[] { "They improve speed", "They contain memes", "They fix bugs and patch security holes", "They add games" }, CorrectOptionIndex = 2, Explanation = "Updates often fix security flaws and keep your device safe." },
                new QuizQuestion { Question = "What is a firewall?", Options = new[] { "An antivirus", "A fake site", "A scam", "A network security system" }, CorrectOptionIndex = 3, Explanation = "Correct! Firewalls monitor and control traffic." },
                new QuizQuestion { Question = "What is social engineering?", Options = new[] { "Building websites", "A phishing scam", "Manipulating people to give up confidential info", "Coding security systems" }, CorrectOptionIndex = 2, Explanation = "Social engineering relies on tricking humans." },
                new QuizQuestion { Question = "True or False: It's okay to share your OTP with a friend.", Options = new[] { "True", "False" }, CorrectOptionIndex = 1, Explanation = "False. OTPs are meant for you only — never share them." }
            };
        }

        private void DisplayQuestion()
        {
            questionAnswered = false;
            FeedbackText.Text = "";
            OptionButtons.Items.Clear();

            if (currentIndex >= questions.Count)
            {
                ShowFinalScore();
                return;
            }

            var q = questions[currentIndex];
            QuestionText.Text = $"Q{currentIndex + 1}: {q.Question}";

            for (int i = 0; i < q.Options.Length; i++)
            {
                var btn = new Button
                {
                    Content = q.Options[i],
                    Tag = i,
                    Margin = new Thickness(5),
                    FontSize = 14
                };
                btn.Click += Option_Click;
                OptionButtons.Items.Add(btn);
            }
        }

        private void Option_Click(object sender, RoutedEventArgs e)
        {
            if (questionAnswered) return;

            if (sender is Button button && button.Tag is int selected)
            {
                var q = questions[currentIndex];
                questionAnswered = true;

                if (selected == q.CorrectOptionIndex)
                {
                    score++;
                    FeedbackText.Foreground = System.Windows.Media.Brushes.Green;
                    FeedbackText.Text = "✅ Correct! " + q.Explanation;
                }
                else
                {
                    FeedbackText.Foreground = System.Windows.Media.Brushes.Red;
                    FeedbackText.Text = "❌ Incorrect. " + q.Explanation;
                }
            }
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            currentIndex++;
            DisplayQuestion();
        }

        private void ShowFinalScore()
        {
            QuestionText.Text = $"Quiz Complete! You scored {score} out of {questions.Count}.";
            OptionButtons.Items.Clear();
            FeedbackText.Text = score >= 8
                ? "🌟 Great job! You're a cybersecurity pro!"
                : score >= 5
                    ? "👍 Not bad! Keep learning to stay safe online."
                    : "📚 Keep studying — your online safety matters!";
        }
    }
}
