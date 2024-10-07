using E_Learning.Repository;
using MediatR;

namespace E_Learning.Commands
{
    public class UpdateQuestion : IRequest<bool>
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string? Hint { get; set; }

        public string Answer { get; set; }
    }

    public class UpdateQuestionHandler : IRequestHandler<UpdateQuestion, bool>
    {
        private readonly IQuestionRepository _questionRepository;

        public UpdateQuestionHandler(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<bool> Handle(UpdateQuestion request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetById(request.Id);

            if (question == null)
                return false;

            question.Text = request.Question;
            question.Hint = request.Hint;
            question.Answer = request.Answer;

            await _questionRepository.Update(question);

            return true;
        }
    }
}