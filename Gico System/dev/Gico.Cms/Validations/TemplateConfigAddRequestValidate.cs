using FluentValidation;
using Gico.SystemModels.Request.PageBuilder;

namespace Gico.Cms.Validations
{
    public class TemplateConfigAddRequestValidate : AbstractValidator<TemplateConfigAddOrChangeRequest>
    {
        public TemplateConfigAddRequestValidate()
        {
            RuleFor(x => x.TemplateId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.TemplatePositionCode).NotNull().NotEmpty().Length(1, 150);
            RuleFor(x => x.PathToView).NotNull().NotEmpty().Length(1, 2048);
            RuleFor(x => x.ComponentType).IsInEnum();
            RuleFor(x => x.ComponentId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.ComponentId).NotNull().NotEmpty();
            RuleFor(x => x.Status).IsInEnum();

        }

        public static FluentValidation.Results.ValidationResult ValidateModel(TemplateConfigAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new TemplateConfigAddRequestValidate().Validate(request);
            return validationResult;
        }
    }
}