using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakeAQuiz.Server.Models
{
    public class GameModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public virtual QuizModel? Quiz { get; set; }

        [ForeignKey("Player")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? Player { get; set; }

        [Required]
        public string Score { get; set; }
    }
}
