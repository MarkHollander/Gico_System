using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class RoleAddRequestValidator : AbstractValidator<RoleAddRequest>
    {
        public RoleAddRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 150);
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(RoleAddRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new RoleAddRequestValidator().Validate(request);
            return validationResult;
        }
    }
}