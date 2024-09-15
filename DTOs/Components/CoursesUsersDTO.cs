using E_Learning.DB.Models;

namespace E_Learning.DTOs.Components
{
    public class CoursesUsersDTO
    {
        public Users User { get; set; }
        public DateTime EnrollDate { get; set; }
        public TimeSpan ActiveTime { get; set; }
    }
}