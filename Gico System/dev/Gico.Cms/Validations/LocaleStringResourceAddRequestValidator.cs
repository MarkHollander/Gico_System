
using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class LocaleStringResourceAddRequestValidator : AbstractValidator<LocaleStringResourceAddRequest>
    {
        public LocaleStringResourceAddRequestValidator()
        {
            RuleFor(x => x.ResourceName).NotNull().NotEmpty().Length(3, 150);
            RuleFor(x => x.ResourceValue).NotNull().NotEmpty().Length(3, 150);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(LocaleStringResourceAddRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new LocaleStringResourceAddRequestValidator().Validate(request);
            return validationResult;
        }
    }
}