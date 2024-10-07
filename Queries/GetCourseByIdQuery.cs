using AutoMapper;
using E_Learning.DB.Models.AdminComponentsDTOs;
using E_Learning.DTOs.Responses;
using E_Learning.Repository;
using MediatR;
using static E_Learning.Constants;

namespace E_Learning.Queries
{
    public class GetCourseDetailsQuery : IRequest<CourseListItemDTO?>
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public string UserRole { get; set; }
    }

    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseDetailsQuery, CourseListItemDTO?>
    {
        private readonly ICoursesRepository corsesRepository;
        private readonly IMapper _mapper;

        public GetCourseByIdQueryHandler(ICoursesRepository corsesRepository, IMapper mapper)
        {
            this.corsesRepository = corsesRepository;
            _mapper = mapper;
        }

        public async Task<CourseListItemDTO?> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request.UserRole == Roles.Teacher.ToString())
            {
                var teacherCourse = await corsesRepository.GetById(request.CourseId);
                var adminCourseDTO = teacherCourse == null ? null : _mapper.Map<AdminCourseDTO>(teacherCourse);
                return adminCourseDTO;
            }

            var studentCourse = await corsesRepository.GetStudentCourse(request.CourseId, request.UserId);
            if (studentCourse == null)
                return null;

            return studentCourse.CoursesUsers != null && studentCourse.CoursesUsers.Count != 0 ?
                _mapper.Map<CourseDTO>(studentCourse) : _mapper.Map<PreviewCourseDTO>(studentCourse);
        }
    }
}