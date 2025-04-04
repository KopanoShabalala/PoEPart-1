using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

class Program
{
    static void Main()
    {
        try
        {
            // 1. Play Voice Greeting
            PlayVoiceGreeting();

            // 2. Display ASCII Art Logo
            DisplayAsciiArt();

            // 3. Welcome the User
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Hello! What is your name? ");
            Console.ResetColor();
            string userName = Console.ReadLine();

            // Input Validation: Ensure name is not empty
            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Name cannot be empty! Please enter your name: ");
                Console.ResetColor();
                userName = Console.ReadLine();
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nWelcome, {userName}! I'm your Cybersecurity Awareness Bot.");
            Console.ResetColor();

            // 4. Chatbot Response System
            ChatbotResponse();

            // End of program
            Console.WriteLine("\nThank you for using the chatbot. Stay safe online!");
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
            // Ensure "greeting.wav" exists in the project folder
            SoundPlayer player = new SoundPlayer("greeting.wav");
            player.Play();
        }
        catch (Exception)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[Audio Error] Could not play the voice greeting.");
            Console.ResetColor();
        }
    }

    static void DisplayAsciiArt()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"
   ____       _                ____        _   
  / ___| ___ | |__   ___ _ __ | __ )  ___ | |_ 
 | |    / _ \| '_ \ / _ \ '_ \|  _ \ / _ \| __|
 | |___| (_) | |_) |  __/ | | | |_) | (_) | |_ 
  \____|\___/|_.__/ \___|_| |_|____/ \___/ \__|

    SecureBot - Your Virtual Cyber Assistant 🛡️
");

        Console.ResetColor();
    }

    static void ChatbotResponse()
    {
        Random rand = new Random();
        bool keepChatting = true;

        string lastTopic = "";
        string userName = "";

        // Get name from earlier input (passed from Main)
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Before we start chatting, what's your name again? ");
        Console.ResetColor();
        userName = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(userName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Please enter a name: ");
            Console.ResetColor();
            userName = Console.ReadLine();
        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"\nAlright {userName}, ask me anything cybersecurity-related!");
        Console.ResetColor();

        var keywordResponses = new Dictionary<string, string>
    {
        { "password", "Make sure your passwords are long, unique, and not reused across accounts." },
        { "phishing", "Phishing is when attackers trick you into giving personal information. Be cautious with emails and links." },
        { "malware", "Malware refers to malicious software designed to harm your device or data." },
        { "2fa", "Two-Factor Authentication (2FA) adds an extra layer of security to your accounts." },
        { "antivirus", "Using updated antivirus software helps detect and block threats." },
        { "scam", "Online scams often involve fake messages or offers. Always verify the source." },
        { "privacy", "Protect your privacy by limiting what personal info you share online." },
        { "firewall", "A firewall blocks unauthorized access to your network or device." }
    };

        while (keepChatting)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nAsk a question (or type 'exit' to quit): ");
            Console.ResetColor();
            string userInput = Console.ReadLine().ToLower();

            // Sentiment Detection
            if (userInput.Contains("sad") || userInput.Contains("upset") || userInput.Contains("depressed"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("I'm sorry to hear you're feeling that way. If you need help, don't hesitate to talk to someone.");
                Console.ResetColor();
            }
            else if (userInput.Contains("angry") || userInput.Contains("mad") || userInput.Contains("frustrated"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("It's okay to feel that way. Let me know how I can assist you better.");
                Console.ResetColor();
            }
            else if (userInput.Contains("confused") || userInput.Contains("don’t understand") || userInput.Contains("lost"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("No worries — I'm here to help. Ask me anything about cybersecurity.");
                Console.ResetColor();
            }
            else if (userInput.Contains("happy") || userInput.Contains("great") || userInput.Contains("awesome") || userInput.Contains("good"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("That's great to hear! Let's keep the positive vibes going. 😊");
                Console.ResetColor();
            }


            if (userInput == "exit")
            {
                keepChatting = false;
                break;
            }

            if (userInput == "who am i?" || userInput == "what's my name?")
            {
                Console.WriteLine($"You are {userName}!");
                continue;
            }

            if (userInput == "what did we talk about?" || userInput == "remind me")
            {
                if (string.IsNullOrEmpty(lastTopic))
                    Console.WriteLine("We haven’t discussed anything specific yet.");
                else
                    Console.WriteLine($"We recently talked about: {lastTopic}");
                continue;
            }

            bool matched = false;

            foreach (var keyword in keywordResponses.Keys)
            {
                if (userInput.Contains(keyword))
                {
                    Console.WriteLine(keywordResponses[keyword]);
                    lastTopic = keyword;
                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                switch (userInput)
                {
                    case "how are you?":
                        string[] howAreYouResponses = {
                        "I'm doing great, thanks for asking!",
                        "Running securely and ready to assist you!",
                        "Just another day in cyberspace!",
                        "All systems go! How can I help?"
                    };
                        Console.WriteLine(howAreYouResponses[rand.Next(howAreYouResponses.Length)]);
                        break;

                    case "thank you":
                    case "thanks":
                        string[] thanksResponses = {
                        "You're welcome!",
                        "Happy to help!",
                        "Stay safe out there!",
                        "Anytime — your security is my priority!"
                    };
                        Console.WriteLine(thanksResponses[rand.Next(thanksResponses.Length)]);
                        break;

                    case "what’s your purpose?":
                        string[] purposeResponses = {
                        "I'm your virtual cybersecurity assistant!",
                        "My job is to keep you informed and safe online.",
                        "I help users understand digital threats and how to avoid them.",
                        "I'm here to spread awareness and security wisdom!"
                    };
                        Console.WriteLine(purposeResponses[rand.Next(purposeResponses.Length)]);
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("I'm not sure about that. Try asking something related to cybersecurity.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
    