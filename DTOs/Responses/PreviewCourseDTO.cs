namespace E_Learning.DTOs.Responses
{
    public class PreviewCourseDTO : CourseListItemDTO
    {
        public bool Enrolled
        {
            get { return false; }
        }

        public List<string> SectionTitles { set; get; }
    }
}