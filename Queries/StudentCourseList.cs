using AutoMapper;
using E_Learning.DTOs.Responses;
using E_Learning.Repository;
using MediatR;

namespace E_Learning.Queries
{
    public class StudentCourseList : IRequest<List<UsersCourseListDTO>>
    {
        public string Id { get; set; }

        public string Role { get; set; }
    }

    public class CourseListHandler : IRequestHandler<StudentCourseList, List<UsersCourseListDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;

        public CourseListHandler(IMapper mapper, ICoursesRepository coursesRepository)
        {
            _mapper = mapper;
            _coursesRepository = coursesRepository;
        }

        public async Task<List<UsersCourseListDTO>> Handle(StudentCourseList request, CancellationToken cancellationToken)
        {
            var userList = await _coursesRepository.GetUserCoursesList(int.Parse(request.Id));
            var userResult = _mapper.Map<List<UsersCourseListDTO>>(userList).ToList();
            return userResult;
        }
    }
}