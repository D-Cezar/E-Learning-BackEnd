using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Learning.DB.Models
{
    public class CourseSections
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { set; get; }

        public string VideoSource { set; get; }

        public string TextSource { set; get; }

        public string Title { set; get; }

        public int CourseId { get; set; }

        [JsonIgnore]
        public Courses Course { get; set; } = null!;

        public List<Questions> Questions { get; set; }
    }
}