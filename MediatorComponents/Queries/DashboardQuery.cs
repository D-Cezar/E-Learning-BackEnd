using E_Learning.DTOs.Responses;
using E_Learning.Repository;
using MediatR;
using static E_Learning.Constants;

namespace E_Learning.MediatorComponents.Queries
{
    public class DashboardQuery : IRequest<DashboardDTO?>
    {
        public int CourseId { get; set; }

        public string Role { get; set; }
    }

    public class DashboradHandler : IRequestHandler<DashboardQuery, DashboardDTO?>
    {
        private readonly ICoursesUsersRepository _coursesUsersRepository;
        private readonly IUserAnswerRepository _userAnswerRepository;

        public DashboradHandler(ICoursesUsersRepository coursesUsersRepository, IUserAnswerRepository userAnswerRepository)
        {
            _coursesUsersRepository = coursesUsersRepository;
            _userAnswerRepository = userAnswerRepository;
        }

        public async Task<DashboardDTO> Handle(DashboardQuery request, CancellationToken cancellationToken)
        {
            if (request.Role != Roles.Teacher.ToString())
                return null;

            var answersProcentage = await _userAnswerRepository.CorrectAnswersPercentage(request.CourseId);

            var sectionsProcentage = await _coursesUsersRepository.CompletedSectionsPercentages(request.CourseId);

            var courseCompletedProcentage = await _coursesUsersRepository.CompletedCoursePercentage(request.CourseId);

            var averageTime = await _coursesUsersRepository.AverageTimeSpent(request.CourseId);

            var response = new DashboardDTO()
            {
                RightAnswersProcentage = answersProcentage,
                FisnishedCourseProcentage = courseCompletedProcentage,
                SectionFinishedProcentage = sectionsProcentage,
                AverageTime = averageTime
            };

            return response;
        }
    }
}