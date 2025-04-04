using System;
using System.Media;
using System.Threading;

class Program
{
    static void Main()

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
        bool keepChatting = true;

        while (keepChatting)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nAsk me a cybersecurity question (or type 'exit' to quit): ");
            Console.ResetColor();
            string userInput = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid question.");
                Console.ResetColor();
                continue;
            }

            if (userInput == "exit")
            {
                keepChatting = false;
                break;
            }

            // Responses
            switch (userInput)
            {
                case "how are you?":
                    Console.WriteLine("I'm just a bot, but I'm always ready to help!");
                    break;

                case "what’s your purpose?":
                    Console.WriteLine("I’m here to educate you about cybersecurity and staying safe online.");
                    break;

                case "what can i ask you about?":
                    Console.WriteLine("You can ask me about password safety, phishing, and safe browsing.");
                    break;

                case "tell me about password safety":
                    Console.WriteLine("Use strong, unique passwords for each site. Consider using a password manager.");
                    break;

                case "what is phishing?":
                    Console.WriteLine("Phishing is a cyber attack where hackers trick you into revealing personal info.");
                    break;

                case "how do i browse safely?":
                    Console.WriteLine("Use HTTPS sites, avoid suspicious links, and enable 2FA on accounts.");
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