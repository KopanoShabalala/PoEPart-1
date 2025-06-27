using System;
using System.Collections.Generic;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading;

class Program
{
    static void Main()
    {
        try
        {
            PlayVoiceGreeting();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Hello! What is your name? ");
            Console.ResetColor();
            string userName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Name cannot be empty! Please enter your name: ");
                Console.ResetColor();
                userName = Console.ReadLine();
            }

            DisplayAsciiArt(userName);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nWelcome, {userName}! I'm SecureBot, your Cybersecurity Assistant.\n");
            Console.ResetColor();

            ChatbotResponse(userName);

            Console.WriteLine("\nThank you for chatting. Stay safe and secure online!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[Unexpected Error] Something went wrong: " + ex.Message);
            Console.ResetColor();
        }
    }

    static void PlayVoiceGreeting()
    {
        try
        {
            SoundPlayer player = new SoundPlayer("greeting.wav");
            player.Play();
        }
        catch
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[Audio Error] Could not play voice greeting.");
            Console.ResetColor();
        }
    }

    static void DisplayAsciiArt(string userName)
    {
        string[] artLines = new string[]
        {
            @"╔══════════════════════════════════════════════════════════════════════╗",
            @"║   ██████╗██╗   ██╗██████╗ ███████╗███████╗██╗   ██╗███████╗████████╗  ║",
            @"║  ██╔════╝██║   ██║██╔══██╗██╔════╝██╔════╝██║   ██║██╔════╝╚══██╔══╝  ║",
            @"║  ██║     ██║   ██║██████╔╝█████╗  █████╗  ██║   ██║█████╗     ██║     ║",
            @"║  ██║     ██║   ██║██╔═══╝ ██╔══╝  ██╔══╝  ██║   ██║██╔══╝     ██║     ║",
            @"║  ╚██████╗╚██████╔╝██║     ███████╗██║     ╚██████╔╝███████╗   ██║     ║",
            @"║   ╚═════╝ ╚═════╝ ╚═╝     ╚══════╝╚═╝      ╚═════╝ ╚══════╝   ╚═╝     ║",
            @"║                                                                      ║",
            @"║     ██████╗██╗   ██╗████████╗ ██████╗ ██████╗  █████╗ ████████╗       ║",
            @"║    ██╔════╝██║   ██║╚══██╔══╝██╔═══██╗██╔══██╗██╔══██╗╚══██╔══╝       ║",
            @"║    ██║     ██║   ██║   ██║   ██║   ██║██████╔╝███████║   ██║          ║",
            @"║    ██║     ██║   ██║   ██║   ██║   ██║██╔═══╝ ██╔══██║   ██║          ║",
            @"║    ╚██████╗╚██████╔╝   ██║   ╚██████╔╝██║     ██║  ██║   ██║          ║",
            @"║     ╚═════╝ ╚═════╝    ╚═╝    ╚═════╝ ╚═╝     ╚═╝  ╚═╝   ╚═╝          ║",
            $"║                    👤 Welcome {userName} — Let's secure your future! 🛡️              ║",
            @"╚══════════════════════════════════════════════════════════════════════╝"
        };

        Console.ForegroundColor = ConsoleColor.Green;

        foreach (string line in artLines)
        {
            Console.WriteLine(line);
            Thread.Sleep(75); // Animate each line
        }

        Console.ResetColor();
    }

    static void ChatbotResponse(string userName)
    {
        Random rand = new Random();
        bool keepChatting = true;
        string lastTopic = "";
        List<string> discussedTopics = new List<string>();

        var keywordResponses = new Dictionary<string[], string>
        {
            { new[] { "password", "passcode", "login credentials" },
              "Use complex passwords with symbols, numbers, and upper/lowercase letters. Consider a password manager!" },

            { new[] { "phishing", "email scam", "fake link" },
              "Phishing is a trick to steal info via fake emails or links. Always verify the sender!" },

            { new[] { "ransomware", "encrypted files", "pay ransom" },
              "Ransomware locks your data and demands payment. Always keep offline backups!" },

            { new[] { "vpn", "virtual private network" },
              "VPNs encrypt your traffic and hide your IP, keeping your browsing private." },

            { new[] { "2fa", "two factor authentication" },
              "2FA adds an extra layer of security beyond your password. Always enable it!" },

            { new[] { "malware", "virus", "spyware", "trojan" },
              "Malware refers to malicious software. Avoid suspicious downloads and use antivirus." },

            { new[] { "firewall", "network protection" },
              "A firewall filters traffic to block unauthorized access to your network." }
        };

        while (keepChatting)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nAsk me anything (type 'exit' to quit): ");
            Console.ResetColor();
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "exit")
            {
                keepChatting = false;
                break;
            }

            if (DetectAndRespondToEmotion(userInput, rand)) continue;

            if (userInput.Contains("who am i") || userInput.Contains("what's my name"))
            {
                Console.WriteLine($"You are {userName}, of course! 😊");
                continue;
            }

            if (userInput.Contains("what did we talk about") || userInput.Contains("remind me"))
            {
                if (discussedTopics.Count == 0)
                    Console.WriteLine("We haven’t discussed any specific topics yet.");
                else
                    Console.WriteLine("So far, we've talked about: " + string.Join(", ", discussedTopics));
                continue;
            }

            bool matched = false;
            foreach (var pair in keywordResponses)
            {
                foreach (var keyword in pair.Key)
                {
                    if (Regex.IsMatch(userInput, @"\b" + Regex.Escape(keyword) + @"\b"))
                    {
                        Console.WriteLine(pair.Value);
                        lastTopic = keyword;
                        discussedTopics.Add(keyword);
                        matched = true;

                        Console.Write("\nWould you like to know more about this topic? (yes/no): ");
                        string more = Console.ReadLine().ToLower();
                        if (more == "yes")
                        {
                            Console.WriteLine("Here's a tip: Stay updated with cybersecurity news to stay ahead of new threats.");
                        }
                        break;
                    }
                }
                if (matched) break;
            }

            if (!matched)
            {
                switch (userInput)
                {
                    case "how are you?":
                        string[] howAreYou = {
                            "I'm secure and ready to assist! 💻",
                            "All systems running smoothly!",
                            "Never been better — the cyber world awaits!"
                        };
                        Console.WriteLine(howAreYou[rand.Next(howAreYou.Length)]);
                        break;

                    case "thank you":
                    case "thanks":
                        string[] thanks = {
                            "You're very welcome!",
                            "No problem at all!",
                            "Always happy to help you stay safe. 😊"
                        };
                        Console.WriteLine(thanks[rand.Next(thanks.Length)]);
                        break;

                    case "what’s your purpose?":
                        Console.WriteLine("I'm SecureBot, created to teach and guide users on how to stay safe in the digital world.");
                        break;

                    case "help":
                        Console.WriteLine("Try asking me about passwords, phishing, ransomware, VPNs, 2FA, firewalls, or malware.");
                        break;

                    default:
                        string[] fallback = {
                            "Hmm... I’m still learning about that. Try something cybersecurity-related.",
                            "Can you rephrase that?",
                            "That’s interesting! But I focus on online safety topics."
                        };
                        Console.WriteLine(fallback[rand.Next(fallback.Length)]);
                        break;
                }
            }
        }
    }

    static bool DetectAndRespondToEmotion(string input, Random rand)
    {
        string[] sad = {
            "I'm sorry you're feeling down. If you need help, reach out to someone you trust.",
            "You're not alone. I'm here with you.",
            "Sending you some virtual strength. 💪"
        };

        string[] angry = {
            "Take a deep breath. Let's focus on something useful!",
            "I'm here to support, not to stress you more.",
            "Let’s work through this calmly together."
        };

        string[] confused = {
            "It’s okay to be confused. Ask me anything — I’ll try to help!",
            "Cybersecurity can be tricky, but you’ve got this.",
            "Don't worry, I’ll explain it simply."
        };

        string[] happy = {
            "Love to hear that! Let’s keep the energy high!",
            "That’s awesome! 😊",
            "Yay! Let’s keep the good vibes going!"
        };

        string[] scared = {
            "Fear is a natural response. Let's take small steps to understand things.",
            "You’re safe here. Ask me anything.",
            "Let's learn together to turn fear into confidence!"
        };

        if (input.Contains("sad") || input.Contains("depressed") || input.Contains("down"))
        {
            Console.WriteLine(sad[rand.Next(sad.Length)]);
            return true;
        }

        if (input.Contains("angry") || input.Contains("mad") || input.Contains("frustrated"))
        {
            Console.WriteLine(angry[rand.Next(angry.Length)]);
            return true;
        }

        if (input.Contains("confused") || input.Contains("don’t understand") || input.Contains("lost"))
        {
            Console.WriteLine(confused[rand.Next(confused.Length)]);
            return true;
        }

        if (input.Contains("happy") || input.Contains("excited") || input.Contains("awesome"))
        {
            Console.WriteLine(happy[rand.Next(happy.Length)]);
            return true;
        }

        if (input.Contains("scared") || input.Contains("afraid") || input.Contains("anxious"))
        {
            Console.WriteLine(scared[rand.Next(scared.Length)]);
            return true;
        }

        return false;
    }
}
