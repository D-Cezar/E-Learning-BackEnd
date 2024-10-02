using E_Learning.Repository;
using MediatR;
using static E_Learning.Constants;

namespace E_Learning.MiddleComponents.Commands
{
    public class ActiveStatusSwitchCourse : IRequest<HttpResponseEnum>
    {
        public int CourseId { get; set; }

        public int UserId { get; set; }
    }

    public class ActiveStatusSwitchCourseHandler : IRequestHandler<ActiveStatusSwitchCourse, HttpResponseEnum>
    {
        private readonly ICoursesRepository _coursesRepository;

        public async Task<HttpResponseEnum> Handle(ActiveStatusSwitchCourse request, CancellationToken cancellationToken)
        {
            var course = await _coursesRepository.GetById(request.CourseId);
            if (course == null)
                return HttpResponseEnum.NotFound;

            if (course.AuthorId != request.UserId)
                return HttpResponseEnum.Unauthorized;

            course.Deactivated_At = course.Deactivated_At == null ? DateTime.UtcNow : null;
            return HttpResponseEnum.Ok;
        }
    }
}