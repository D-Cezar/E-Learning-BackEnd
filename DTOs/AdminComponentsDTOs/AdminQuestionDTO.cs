namespace E_Learning.DB.Models.AdminComponentsDTOs
{
    public class AdminQuestionDTO
    {
        public int Id { set; get; }

        public string Text { set; get; }

        public string? Hint { set; get; }

        public string Answer { set; get; }
    }
}