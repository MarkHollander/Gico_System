using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class RoleChangeRequestValidator : AbstractValidator<RoleChangeRequest>
    {
        public RoleChangeRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.LanguageDefaultId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 150);
            RuleFor(x => x.Status);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(RoleChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new RoleChangeRequestValidator().Validate(request);
            return validationResult;
        }
    }
}