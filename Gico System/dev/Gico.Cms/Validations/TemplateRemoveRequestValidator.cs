using FluentValidation;
using Gico.SystemModels.Request.PageBuilder;

namespace Gico.Cms.Validations
{
    public class TemplateRemoveRequestValidator : AbstractValidator<TemplateRemoveRequest>
    {
        public TemplateRemoveRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(TemplateRemoveRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new TemplateRemoveRequestValidator().Validate(request);
            return validationResult;

        }
    }
}