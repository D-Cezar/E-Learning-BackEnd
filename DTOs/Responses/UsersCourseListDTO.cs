namespace E_Learning.DTOs.Responses
{
    public class UsersCourseListDTO : CourseListItemDTO
    {
        public bool Enrolled { set; get; }

        public bool Completed { set; get; }
    }
}