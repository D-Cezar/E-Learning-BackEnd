using E_Learning.Repository;
using MediatR;

namespace E_Learning.MediatorComponents.Commands
{
    public class DeleteQuestion : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeteleQuestionHandler : IRequestHandler<DeleteQuestion, bool>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserAnswerRepository _userAnswerRepository;

        public DeteleQuestionHandler(IQuestionRepository questionRepository, IUserAnswerRepository userAnswerRepository)
        {
            _questionRepository = questionRepository;
            _userAnswerRepository = userAnswerRepository;
        }

        public async Task<bool> Handle(DeleteQuestion request, CancellationToken cancellationToken)
        {
            await _userAnswerRepository.DeleteQuestionAnswers(request.Id);
            return await _questionRepository.DeleteById(request.Id);
        }
    }
}