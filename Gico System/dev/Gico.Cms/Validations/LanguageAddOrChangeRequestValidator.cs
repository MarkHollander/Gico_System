using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class LanguageAddOrChangeRequestValidator : AbstractValidator<LanguageAddOrChangeRequest>
    {
        public LanguageAddOrChangeRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Culture).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(x => x.DisplayOrder).NotNull().GreaterThan(0);
            RuleFor(x => x.Published).NotNull();
            RuleFor(x => x.UniqueSeoCode).MaximumLength(2);
            
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(LanguageAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new LanguageAddOrChangeRequestValidator().Validate(request);
            return validationResult;
        }
    }
}