using E_Learning.Repository;
using MediatR;

namespace E_Learning.MediatorComponents.Commands
{
    public class UpdateCourseUser : IRequest<bool>
    {
        public int CourseId { get; set; }

        public int UserId { get; set; }

        public List<int> SectionsId { get; set; }

        public TimeSpan? ActiveTime { get; set; }
    }

    public class UpdateCourseUserHandler : IRequestHandler<UpdateCourseUser, bool>
    {
        private readonly ICoursesUsersRepository _coursesUsersRepository;
        private readonly ICourseSectionRepository _courseSectionRepository;
        private readonly ICoursesRepository _coursesRepository;

        public UpdateCourseUserHandler(ICoursesUsersRepository coursesUsersRepository, ICourseSectionRepository courseSectionRepository, ICoursesRepository coursesRepository)
        {
            _coursesUsersRepository = coursesUsersRepository;
            _courseSectionRepository = courseSectionRepository;
            _coursesRepository = coursesRepository;
        }

        public async Task<bool> Handle(UpdateCourseUser request, CancellationToken cancellationToken)
        {
            var enrolledCourse = await _coursesRepository.GetById(request.CourseId);
            if (enrolledCourse == null)
                return false;
            var checkIfIdsMatch = await _courseSectionRepository.VerifyMatchForSectionIds(request.CourseId, request.SectionsId);
            if (!checkIfIdsMatch)
                return false;

            var courseUser = await _coursesUsersRepository.GetCourseForUser(request.UserId, request.CourseId);

            courseUser.ActiveTime = courseUser.ActiveTime != null && request.ActiveTime!.HasValue ?
                courseUser.ActiveTime + request.ActiveTime!.Value : request.ActiveTime!.Value;

            var uniqueSections = new HashSet<int>(request.SectionsId);
            courseUser.CompletedSectionIds = uniqueSections.ToList();

            await _coursesUsersRepository.Update(courseUser);

            return true;
        }
    }
}