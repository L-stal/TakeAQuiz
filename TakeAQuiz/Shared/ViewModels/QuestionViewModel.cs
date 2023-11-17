using System.Diagnostics.CodeAnalysis;

namespace TakeAQuiz.Shared.ViewModels
{
    public class QuestionViewModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Media { get; set; }
        public int TimeLimit { get; set; }
        //Multi and timeLimit to handel states in frontend
		public bool MultiAnswer { get; set; }
        public bool HasTimeLimit { get; set; }

		public virtual List<MockViewModel> MockAnswers { get; set; } = new List<MockViewModel>();
    }
}