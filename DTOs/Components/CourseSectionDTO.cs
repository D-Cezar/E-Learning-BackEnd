namespace E_Learning.DTOs.Components
{
    public class CourseSectionDTO
    {
        public int Id { set; get; }

        public string VideoSource { set; get; }

        public string TextSource { set; get; }

        public string Title { set; get; }

        public int CourseId { get; set; }

        public List<QuestionDTO> Questions { get; set; }
    }
}