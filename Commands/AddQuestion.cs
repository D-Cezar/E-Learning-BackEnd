using E_Learning.DB.Models;
using E_Learning.Repository;
using MediatR;

namespace E_Learning.Commands
{
    public class AddQuestion : IRequest<bool>
    {
        public int SectionId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string? Hint { get; set; }
    }

    public class AddQuestionHandler : IRequestHandler<AddQuestion, bool>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ICourseSectionRepository _courseSectionRepository;

        public AddQuestionHandler(IQuestionRepository questionRepository, ICourseSectionRepository courseSectionRepository)
        {
            _questionRepository = questionRepository;
            _courseSectionRepository = courseSectionRepository;
        }

        public async Task<bool> Handle(AddQuestion request, CancellationToken cancellationToken)
        {
            var section = await _courseSectionRepository.GetById(request.SectionId);

            if (section == null)
                return false;

            var question = new Questions
            {
                Text = request.Question,
                Answer = request.Answer,
                Hint = request.Hint,
                SectionCourse = section
            };

            await _questionRepository.Add(question);

            return true;
        }
    }
}