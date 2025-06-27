namespace CyberChatBotWPF
{
    public class QuizQuestion
    {
        public string Question { get; set; } = "";
        public string[] Options { get; set; } = new string[0];
        public int CorrectOptionIndex { get; set; }
        public string Explanation { get; set; } = "";
    }
}
