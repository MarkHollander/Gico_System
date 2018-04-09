

using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class LocaleStringResourceChangeRequestValidator : AbstractValidator<LocaleStringResourceChangeRequest>
    {
        public LocaleStringResourceChangeRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.ResourceName).NotNull().NotEmpty().Length(3, 150);
            RuleFor(x => x.ResourceValue).NotNull().NotEmpty().Length(3, 150);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(LocaleStringResourceChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new LocaleStringResourceChangeRequestValidator().Validate(request);
            return validationResult;
        }
    }
}