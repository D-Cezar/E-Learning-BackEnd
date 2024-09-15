using E_Learning.DB.Models.AdminComponentsDTOs;

namespace E_Learning.DTOs.AdminComponentsDTOs
{
    public class AdminCourseSectionDTO
    {
        public int Id { set; get; }

        public string TextSource { set; get; }

        public string Title { set; get; }

        public int CourseId { get; set; }

        public List<AdminQuestionDTO> Questions { get; set; }
    }
}