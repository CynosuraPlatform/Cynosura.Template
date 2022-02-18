using FluentValidation;
using Microsoft.Extensions.Localization;
using Cynosura.Template.Core.Infrastructure.Validators;
using Cynosura.Template.Core.Workers;

namespace Cynosura.Template.Core.Requests.WorkerInfos
{
    public class UpdateWorkerInfoValidator : AbstractValidator<UpdateWorkerInfo>
    {
        public UpdateWorkerInfoValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(200).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.ClassName).MaximumLength(200).NotEmpty().ClassNameImplementsInterface(typeof(IWorker)).WithName(x => localizer["Class Name"]);
        }

    }
}
