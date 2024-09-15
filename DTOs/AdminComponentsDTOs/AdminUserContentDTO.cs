using E_Learning.DTOs.Responses;

namespace E_Learning.DB.Models.AdminComponentsDTOs
{
    public class AdminUserContentDTO : UserDTO
    {
        public List<string> CompletedSectionTitles { get; set; }

        public TimeSpan? ActiveTime { get; set; }

        public DateTime EnrollDate { get; set; }

        public List<AdminAnswerDTO> QuestionsAndAnswers { get; set; }
    }
}