using E_Learning.DTOs.Components;

namespace E_Learning.DTOs.Responses
{
    public class CourseDTO : UsersCourseListDTO
    {
        public List<CourseSectionDTO> Sections { get; set; }

        public List<int> CompletedSectionIds { get; set; }
    }
}