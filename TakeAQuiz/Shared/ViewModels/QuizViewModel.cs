
namespace TakeAQuiz.Shared.ViewModels
{
    public class QuizViewModel
    {
        public string Title { get; set; }
        public int MaxScore { get; set; }
        public int? GamesPlayed { get; set; }
        public int? OverallRating { get; set; }
        public virtual List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
    }
}
