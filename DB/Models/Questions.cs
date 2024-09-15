using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Learning.DB.Models
{
    public class Questions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { set; get; }

        [Required]
        public string Text { set; get; }

        [Required]
        public string Answer { set; get; }

        public string? Hint { set; get; }

        [JsonIgnore]
        public CourseSections SectionCourse { set; get; }

        public List<UserAnswers> UserAnswers { set; get; }
    }
}