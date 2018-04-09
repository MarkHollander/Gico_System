using FluentValidation;
using Gico.SystemModels.Request.PageBuilder;

namespace Gico.Cms.Validations
{
    public class TemplateConfigChangeRequestValidate : AbstractValidator<TemplateConfigAddOrChangeRequest>
    {
        public TemplateConfigChangeRequestValidate()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.TemplateId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.TemplatePositionCode).NotNull().NotEmpty().Length(1, 150);
            RuleFor(x => x.ComponentType).IsInEnum();
            RuleFor(x => x.ComponentId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.ComponentId).NotNull().NotEmpty();
            RuleFor(x => x.Status).IsInEnum();

        }

        public static FluentValidation.Results.ValidationResult ValidateModel(TemplateConfigAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new TemplateConfigChangeRequestValidate().Validate(request);
            return validationResult;
        }
    }
}