using E_Learning.Repository;
using MediatR;

namespace E_Learning.MiddleComponents.Commands
{
    public class AddSession : IRequest<bool>
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        public TimeSpan ActiveTime { get; set; }
    }

    public class AddSessionCommandHandler : IRequestHandler<AddSession, bool>
    {
        private readonly ICoursesUsersRepository _coursesUsersRepository;

        public AddSessionCommandHandler(ICoursesUsersRepository coursesUsersRepository)
        {
            _coursesUsersRepository = coursesUsersRepository;
        }

        public async Task<bool> Handle(AddSession request, CancellationToken cancellationToken)
        {
            var enrolledCourse = await _coursesUsersRepository.GetCourseForUser(request.UserId, request.CourseId);
            if (enrolledCourse == null)
            {
                return false;
            }

            enrolledCourse.ActiveTime = enrolledCourse.ActiveTime != null ? enrolledCourse.ActiveTime + request.ActiveTime : request.ActiveTime;
            await _coursesUsersRepository.Update(enrolledCourse);

            return true;
        }
    }
}