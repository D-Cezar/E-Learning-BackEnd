using AutoMapper;
using E_Learning.DB.Models.AdminComponentsDTOs;
using E_Learning.Repository;
using MediatR;

namespace E_Learning.MediatorComponents.Queries
{
    public class ActiveUsersQuery : IRequest<List<AdminUserContentDTO>>
    {
        public int CourseId { get; set; }
    }

    public class ActiveUsersHandler : IRequestHandler<ActiveUsersQuery, List<AdminUserContentDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseSectionRepository _courseSectionRepository;
        private readonly IMapper _mapper;

        public ActiveUsersHandler(IUserRepository userRepository, IMapper mapper, ICourseSectionRepository courseSectionRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _courseSectionRepository = courseSectionRepository;
        }

        public async Task<List<AdminUserContentDTO>> Handle(ActiveUsersQuery request, CancellationToken cancellationToken)
        {
            var activeUsersList = await _userRepository.GetActiveUsers(request.CourseId);

            var response = _mapper.Map<List<AdminUserContentDTO>>(activeUsersList);

            foreach (var user in response)
            {
                var completedSections = await _courseSectionRepository.CompletedSectionTitles(request.CourseId, user.Id);

                user.CompletedSectionTitles = completedSections;
            }

            return response;
        }
    }
}