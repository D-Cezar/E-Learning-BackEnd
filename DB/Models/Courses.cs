using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.DB.Models
{
    public class Courses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { set; get; }

        [Required]
        public string Title { set; get; }

        public string Description { set; get; }

        public int AuthorId { set; get; }

        public Users Author { set; get; }

        public string Type { set; get; }

        public string ImageSourse { set; get; }

        public List<CourseSections> Sections { get; set; }

        public List<CoursesUsers> CoursesUsers { get; set; }
    }
}