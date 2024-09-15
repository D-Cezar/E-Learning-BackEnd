using AutoMapper;
using E_Learning.DTOs.Responses;
using E_Learning.Repository;
using MediatR;

namespace E_Learning.MediatorComponents.Queries
{
    public class LastAnswers : IRequest<List<LastAnswerDTO>>
    {
        public int UserId { get; set; }

        public int CourseId { get; set; }
    }

    public class LastAnswerHandler : IRequestHandler<LastAnswers, List<LastAnswerDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserAnswerRepository _userAnswerRepository;

        public LastAnswerHandler(IMapper mapper, IUserAnswerRepository userAnswerRepository)
        {
            _mapper = mapper;
            _userAnswerRepository = userAnswerRepository;
        }

        public async Task<List<LastAnswerDTO>> Handle(LastAnswers request, CancellationToken cancellationToken)
        {
            var lastAnswersList = await _userAnswerRepository.GetUserLastAnswers(request.UserId, request.CourseId);
            var result = _mapper.Map<List<LastAnswerDTO>>(lastAnswersList);

            return result;
        }
    }
}