using E_Learning.Repository;
using MediatR;

namespace E_Learning.MediatorComponents.Commands
{
    public class UpdateCompletedSections : IRequest<bool>
    {
        public int CourseId { get; set; }

        public int UserId { get; set; }

        public int SectionId { get; set; }
    }

    public class UpdateCompletedSectionsHandler : IRequestHandler<UpdateCompletedSections, bool>
    {
        private readonly ICoursesUsersRepository _coursesUsersRepository;
        private readonly ICourseSectionRepository _courseSectionRepository;
        private readonly ICoursesRepository _coursesRepository;

        public UpdateCompletedSectionsHandler(ICoursesUsersRepository coursesUsersRepository, ICourseSectionRepository courseSectionRepository, ICoursesRepository coursesRepository)
        {
            _coursesUsersRepository = coursesUsersRepository;
            _courseSectionRepository = courseSectionRepository;
            _coursesRepository = coursesRepository;
        }

        public async Task<bool> Handle(UpdateCompletedSections request, CancellationToken cancellationToken)
        {
            if (request.CourseId == null || request.UserId == null || request.SectionId == null)
                return false;

            var courseUsersObject = await _coursesUsersRepository.GetCourseForUser(request.UserId, request.CourseId);
            if (courseUsersObject == null)
                return false;

            var courseSection = await _courseSectionRepository.GetById(request.SectionId);
            var course = await _coursesRepository.GetById(request.CourseId);
            if (courseSection != null && courseSection.CourseId == request.CourseId)
            {
                var completedCourse = courseUsersObject.CompletedSectionIds.Count + 1 == course.Sections.Count;
                await _coursesUsersRepository.UpdateCourseCompletion(courseUsersObject, request.SectionId, completedCourse);
                return true;
            }

            return false;
        }
    }
}