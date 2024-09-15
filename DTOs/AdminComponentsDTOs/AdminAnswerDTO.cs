namespace E_Learning.DB.Models.AdminComponentsDTOs
{
    public class AdminAnswerDTO
    {
        public string Question { get; set; }

        public string GivenAnswer { get; set; }

        public DateTime AnswerTime { get; set; }

        public bool IsCorrect { get; set; }
    }
}