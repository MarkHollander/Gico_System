using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class PermissionChangeByRoleRequestValidator : AbstractValidator<PermissionChangeByRoleRequest>
    {
        public PermissionChangeByRoleRequestValidator()
        {
            RuleFor(x => x.RoleId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(PermissionChangeByRoleRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new PermissionChangeByRoleRequestValidator().Validate(request);
            return validationResult;
        }
    }
}