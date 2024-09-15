namespace E_Learning.DTOs.Responses
{
    public class CourseListItemDTO
    {
        public int Id { set; get; }

        public string Title { set; get; }

        public string Description { set; get; }

        public string Author { set; get; }

        public int AuthorId { set; get; }

        public string Type { set; get; }

        public string ImageSourse { set; get; }
    }
}