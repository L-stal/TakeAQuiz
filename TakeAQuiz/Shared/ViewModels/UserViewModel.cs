namespace TakeAQuiz.Shared.ViewModels
{
    public class UserViewModel
    {
        public string? UserName { get; set; }
        public List<QuizViewModel>? Quizzes { get; set; }
    }
}
