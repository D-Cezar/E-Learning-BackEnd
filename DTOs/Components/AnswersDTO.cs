using E_Learning.DB.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.DTOs.Components
{
    public class AnswersDTO
    {
        public int Id { set; get; }

        public string? Anwser { set; get; }

        public List<UserAnswers> UserAnwsers { set; get; }
    }
}