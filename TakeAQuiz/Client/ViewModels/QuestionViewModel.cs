using System.Diagnostics.CodeAnalysis;

namespace TakeAQuiz.Client.ViewModels
{
    public class QuestionViewModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
    }
}
