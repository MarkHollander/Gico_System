using FluentValidation;
using Gico.SystemModels.Request.PageBuilder;

namespace Gico.Cms.Validations
{
    public class TemplateChangeRequestValidator : AbstractValidator<TemplateAddOrChangeRequest>
    {
        public TemplateChangeRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.TemplateName).NotNull().NotEmpty().Length(3, 100);
            RuleFor(x => x.PathToView).NotNull().NotEmpty().Length(1, 2048);
            RuleFor(x => x.PageType).IsInEnum();
            RuleFor(x => x.Status).IsInEnum();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(TemplateAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new TemplateChangeRequestValidator().Validate(request);
            return validationResult;

        }
    }
}