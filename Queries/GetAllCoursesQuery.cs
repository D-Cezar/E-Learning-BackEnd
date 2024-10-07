using E_Learning.DB.Models;
using E_Learning.Repository;
using MediatR;

namespace E_Learning.Queries
{
    public class GetAllCourses : IRequest<List<Courses>>
    {
    }

    public class GetAllCoursesQuery : IRequestHandler<GetAllCourses, List<Courses>>
    {
        private readonly ICoursesRepository _coursesRepository;

        public GetAllCoursesQuery(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<List<Courses>> Handle(GetAllCourses request, CancellationToken cancellationToken)
        {
            return await _coursesRepository.GetAll();
        }
    }
}