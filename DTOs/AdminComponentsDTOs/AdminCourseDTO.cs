using E_Learning.DTOs.AdminComponentsDTOs;
using E_Learning.DTOs.Components;
using E_Learning.DTOs.Responses;

namespace E_Learning.DB.Models.AdminComponentsDTOs
{
    public class AdminCourseDTO : UsersCourseListDTO
    {
        public List<AdminCourseSectionDTO> Sections { get; set; }

        public List<int> CompletedSectionIds { get; set; }

        public List<CoursesUsersDTO> Users { get; set; }

        public List<AnswersDTO> Answers { get; set; }
    }
}