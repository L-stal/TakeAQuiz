using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TakeAQuiz.Server.Models
{
    public class QuizModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int MaxScore { get; set; }

        [AllowNull]
        public int? GamesPlayed { get; set; }

        [AllowNull]
        public int? OverallRating { get; set; }

        public virtual List<QuestionModel> Questions { get; set; } = new List<QuestionModel>();
    }
}
