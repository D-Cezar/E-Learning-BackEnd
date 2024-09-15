using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.DB.Models
{
    public class CoursesUsers
    {
        public int UserId { get; set; }
        public Users User { get; set; }
        public int CourseId { get; set; }

        public Courses Course { get; set; }
        public DateTime EnrollDate { get; set; }
        public TimeSpan? ActiveTime { get; set; }

        [NotMapped]
        public bool Completed
        {
            get
            {
                return Course.Sections.Count == CompletedSectionIds.Count;
            }
        }

        public List<int> CompletedSectionIds { get; set; }
    }
}