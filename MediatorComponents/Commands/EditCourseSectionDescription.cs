using E_Learning.DB.Models;
using E_Learning.Repository;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace E_Learning.MediatorComponents.Commands
{
    public class EditCourseSectionDescription : IRequest<bool>
    {
        public int CourseSectionId { get; set; }
        public string Text { get; set; }

        public string Ttile { get; set; }
    }

    public class EditCourseSectionDescriptionHandler : IRequestHandler<EditCourseSectionDescription, bool>
    {
        private readonly ICourseSectionRepository _courseSectionRepository;

        public EditCourseSectionDescriptionHandler(ICourseSectionRepository courseSectionRepository)
        {
            _courseSectionRepository = courseSectionRepository;
        }

        public async Task<bool> Handle(EditCourseSectionDescription request, CancellationToken cancellationToken)
        {
            var section = await _courseSectionRepository.GetById(request.CourseSectionId);

            if (section == null)
                return false;

            var patchDoc = new JsonPatchDocument<CourseSections>();

            patchDoc.Replace(e => e.TextSource, request.Text);

            patchDoc.Replace(e => e.Title, request.Ttile);

            patchDoc.ApplyTo(section);

            await _courseSectionRepository.Update(section);

            return true;
        }
    }
}