using E_Learning.DB.Models;
using E_Learning.Repository;
using MediatR;

namespace E_Learning.DBElements.Queries
{
    public class GetCoursesByTitleQuery : IRequest<List<Courses>>
    {
        public string Title { get; }

        public GetCoursesByTitleQuery(string title)
        {
            Title = title;
        }
    }

    public class GetCoursesByTitleQueryHandler : IRequestHandler<GetCoursesByTitleQuery, List<Courses>>
    {
        private readonly ICoursesRepository _coursesRepository;

        public GetCoursesByTitleQueryHandler(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<List<Courses>> Handle(GetCoursesByTitleQuery request, CancellationToken cancellationToken)
        {
            return await _coursesRepository.GetCoursesByTitle(request.Title);
        }
    }
}