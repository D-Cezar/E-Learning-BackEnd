using E_Learning.DB.Models;
using E_Learning.DTOs.Components;
using E_Learning.Repository;
using MediatR;

namespace E_Learning.MediatorComponents.Commands
{
    public class AddCourse : IRequest<bool>
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string ImageSourse { get; set; }

        public List<AddCourseSectionDTO> Sections { get; set; }
    }

    public class AddCourseHandler : IRequestHandler<AddCourse, bool>
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ICourseSectionRepository _courseSectionRepository;

        public AddCourseHandler(ICoursesRepository coursesRepository, ICourseSectionRepository courseSectionRepository)
        {
            _coursesRepository = coursesRepository;
            _courseSectionRepository = courseSectionRepository;
        }

        public async Task<bool> Handle(AddCourse request, CancellationToken cancellationToken)
        {
            var course = new Courses
            {
                AuthorId = request.AuthorId,
                Title = request.Title,
                Description = request.Description,
                Type = request.Type,
                ImageSourse = request.ImageSourse,
                Sections = new List<CourseSections>()
            };

            await _coursesRepository.Add(course);
            if (course.Id == 0)
            {
                return false;
            }

            if (request.Sections != null && request.Sections.Count > 0)
            {
                foreach (var sectionDto in request.Sections)
                {
                    var section = new CourseSections
                    {
                        Title = sectionDto.Title,
                        TextSource = sectionDto.TextSource,
                        VideoSource = sectionDto.VideoSource,
                        CourseId = course.Id
                    };

                    await _courseSectionRepository.Add(section);
                }
            }

            return true;
        }
    }
}