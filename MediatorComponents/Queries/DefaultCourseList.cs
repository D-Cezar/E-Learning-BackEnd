using AutoMapper;
using E_Learning.DTOs.Responses;
using E_Learning.Repository;
using MediatR;

namespace E_Learning.MediatorComponents.Queries
{
    public class DefaultCourseList : IRequest<List<CourseListItemDTO>>
    {
        public string? Role { get; set; }

        public string? Id { get; set; }
    }

    public class DefaultCourseListHandler : IRequestHandler<DefaultCourseList, List<CourseListItemDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ICoursesRepository _coursesRepository;

        public DefaultCourseListHandler(IMapper mapper, ICoursesRepository coursesRepository)
        {
            _mapper = mapper;
            _coursesRepository = coursesRepository;
        }

        public async Task<List<CourseListItemDTO>> Handle(DefaultCourseList request, CancellationToken cancellationToken)
        {
            var courseList = await _coursesRepository.GetAll();
            var result = _mapper.Map<List<CourseListItemDTO>>(courseList);

            if (request.Role == null || request.Id == null)
                return result;

            var teacterId = int.Parse(request.Id);
            return result.Where(c => c.AuthorId == teacterId).ToList();
        }
    }
}