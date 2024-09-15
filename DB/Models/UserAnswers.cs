using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.DB.Models
{
    public class UserAnswers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { set; get; }

        public int QuestionId { get; set; }

        public virtual Questions Question { get; set; }

        public int UserId { get; set; }

        public virtual Users User { get; set; }

        public string GivenAnswer { get; set; }

        [NotMapped]
        public bool IsCorrect
        { get { return Question.Answer == GivenAnswer; } }

        public DateTime AnswerTime { get; set; }
    }
}