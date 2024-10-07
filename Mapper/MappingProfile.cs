using AutoMapper;
using E_Learning.DB.Models;
using E_Learning.DTOs.Components;
using E_Learning.DTOs.Responses;
using E_Learning.DB.Models.AdminComponentsDTOs;
using E_Learning.DTOs.AdminComponentsDTOs;
using E_Learning.Commands;

namespace E_Learning.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Courses, AdminCourseDTO>()
                .ForMember(dest => dest.Author, act => act.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
                .ForMember(destination => destination.Users, options => options.MapFrom(course => course.CoursesUsers))
                .ForMember(destination => destination.Answers, options => options.MapFrom(course => course.Sections
                .SelectMany(section => section.Questions).SelectMany(question => question.UserAnswers)));

            CreateMap<Courses, CourseListItemDTO>().ForMember(dest => dest.Author, act => act.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"));

            CreateMap<Courses, UsersCourseListDTO>().IncludeBase<Courses, CourseListItemDTO>()
                .ForMember(dest => dest.Enrolled, act => act.MapFrom(src => src.CoursesUsers != null && src.CoursesUsers.Count != 0))
                .ForMember(dest => dest.Completed, act => act.MapFrom(src => src.CoursesUsers.FirstOrDefault().Completed));

            CreateMap<Courses, CourseDTO>().IncludeBase<Courses, UsersCourseListDTO>()
                .ForMember(dest => dest.CompletedSectionIds, act => act.MapFrom(src => src.CoursesUsers.FirstOrDefault().CompletedSectionIds));
            //.ForMember(dest => dest.Author, act => act.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"));

            CreateMap<Courses, PreviewCourseDTO>().IncludeBase<Courses, CourseListItemDTO>()
                .ForMember(d => d.SectionTitles, act => act.MapFrom(src => src.Sections.Select(sec => sec.Title).ToList()));

            CreateMap<UserAnswers, LastAnswerDTO>();

            CreateMap<Users, UserDTO>();
            CreateMap<UserAnswers, AnswersDTO>();
            CreateMap<CourseSections, CourseSectionDTO>();
            CreateMap<CourseSections, AdminCourseSectionDTO>();
            CreateMap<Questions, QuestionDTO>();
            CreateMap<Questions, AdminQuestionDTO>();
            CreateMap<CoursesUsers, CoursesUsersDTO>();
            CreateMap<AddAnswer, UserAnswers>()
                .ForMember(dest => dest.AnswerTime, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UserAnswers, AdminAnswerDTO>()
                .ForMember(d => d.Question, a => a.MapFrom(s => s.Question.Text))
                .ForMember(d => d.IsCorrect, a => a.MapFrom(s => s.GivenAnswer == s.Question.Answer));

            CreateMap<Users, AdminUserContentDTO>()
              .ForMember(dest => dest.ActiveTime, opt => opt.MapFrom(src =>
        src.CoursesUsers.Any() ? TimeSpan.FromTicks(src.CoursesUsers.Sum(cu => cu.ActiveTime.HasValue ? cu.ActiveTime.Value.Ticks : 0)) : TimeSpan.Zero))
    .ForMember(dest => dest.EnrollDate, opt => opt.MapFrom(src =>
        src.CoursesUsers.Any() ? src.CoursesUsers.Min(cu => cu.EnrollDate) : (DateTime?)null))
    .ForMember(dest => dest.QuestionsAndAnswers, opt => opt.MapFrom(src => src.UserAnswers));
        }
    }
}