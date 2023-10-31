using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TakeAQuiz.Server.Models
{
    public class QuestionModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual GameModel? Game { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

        [AllowNull]
        public string? Image { get; set; }

        [AllowNull]
        public string? Video { get; set; }
    }
}
