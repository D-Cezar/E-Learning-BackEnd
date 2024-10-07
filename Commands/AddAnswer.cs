using AutoMapper;
using E_Learning.DB.Models;
using E_Learning.Repository;
using MediatR;

namespace E_Learning.Commands
{
    public class AddAnswer : IRequest<bool?>
    {
        public int QuestionId { get; set; }

        public int UserId { get; set; }

        public string GivenAnswer { get; set; }
    }

    public class AddAnswerCommandHandler : IRequestHandler<AddAnswer, bool?>
    {
        private readonly IUserAnswerRepository _userAnswerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddAnswerCommandHandler(IUserAnswerRepository repository, IQuestionRepository questionRepository, IMapper mapper, IUserRepository userRepository)
        {
            _userAnswerRepository = repository;
            _questionRepository = questionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool?> Handle(AddAnswer request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetById(request.QuestionId);
            var user = await _userRepository.GetById(request.UserId);

            if (question == null || user == null)
                return null;

            var answer = _mapper.Map<UserAnswers>(request);
            answer.User = user;
            answer.Question = question;

            await _userAnswerRepository.Add(answer);
            return answer.IsCorrect;
        }
    }
}