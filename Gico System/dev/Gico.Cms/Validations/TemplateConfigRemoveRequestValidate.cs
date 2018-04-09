using FluentValidation;
using Gico.SystemModels.Request.PageBuilder;

namespace Gico.Cms.Validations
{
    public class TemplateConfigRemoveRequestValidate : AbstractValidator<TemplateConfigRemoveRequest>
    {
        public TemplateConfigRemoveRequestValidate()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.TemplateId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(TemplateConfigRemoveRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new TemplateConfigRemoveRequestValidate().Validate(request);
            return validationResult;
        }
    }
}