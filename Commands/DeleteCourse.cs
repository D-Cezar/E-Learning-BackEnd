using E_Learning.Repository;
using MediatR;

namespace E_Learning.Commands
{
    public class DeleteCourse : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteCourse(int id)
        {
            Id = id;
        }
    }

    public class DeleteCourseHandler : IRequestHandler<DeleteCourse, bool>
    {
        private readonly ICoursesRepository _coursesRepository;

        public async Task<bool> Handle(DeleteCourse request, CancellationToken cancellationToken)
        {
            var course = await _coursesRepository.GetById(request.Id);
            if (course == null)
                return false;
            await _coursesRepository.DeleteById(request.Id);
            return false;
        }
    }
}