namespace E_Learning.DTOs.Responses
{
    public class DashboardDTO
    {
        public decimal RightAnswersProcentage { get; set; }

        public decimal FisnishedCourseProcentage { get; set; }

        public List<decimal> SectionFinishedProcentage { get; set; }

        public TimeSpan AverageTime { get; set; }
    }
}