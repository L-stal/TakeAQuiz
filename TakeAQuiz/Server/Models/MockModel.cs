using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakeAQuiz.Server.Models
{
    public class MockModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("QuestionId")]
        public int QuestionId { get; set; }
        public virtual QuestionModel? Question { get; set; }

        public string MockAnswer { get; set; }
    }
}
