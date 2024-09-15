using E_Learning.DB.Models;
using E_Learning.Repository;
using MediatR;

namespace E_Learning.MiddleComponents.Commands
{
    public class Enroll : IRequest<bool>
    {
        public int UserId { get; set; }

        public int CourseId { get; set; }
    }

    public class EnrollCommandHandler : IRequestHandler<Enroll, bool>
    {
        private readonly ICoursesUsersRepository _coursesUsersRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICoursesRepository _coursesRepository;

        public EnrollCommandHandler(ICoursesUsersRepository coursesUsersRepository, IUserRepository userRepository, ICoursesRepository coursesRepository)
        {
            _coursesUsersRepository = coursesUsersRepository;
            _userRepository = userRepository;
            _coursesRepository = coursesRepository;
        }

        public async Task<bool> Handle(Enroll request, CancellationToken cancellationToken)
        {
            var course = await _coursesRepository.GetById(request.CourseId);
            var user = await _userRepository.GetById(request.UserId);

            if (course == null || user == null)
                return false;
            var enrollDate = new CoursesUsers
            {
                UserId = request.UserId,
                User = user,
                CourseId = request.CourseId,
                Course = course,
                EnrollDate = DateTime.Now,
                CompletedSectionIds = new List<int>()
            };
            await _coursesUsersRepository.Add(enrollDate);

            return true;
        }
    }
}