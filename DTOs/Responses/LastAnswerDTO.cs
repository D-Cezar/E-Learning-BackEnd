namespace E_Learning.DTOs.Responses
{
    public class LastAnswerDTO
    {
        public int QuestionId { get; set; }
        public string GivenAnswer { get; set; }

        public bool IsCorrect { get; set; }
    }
}