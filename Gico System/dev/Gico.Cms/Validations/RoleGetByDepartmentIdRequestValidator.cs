using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class RoleGetByDepartmentIdRequestValidator : AbstractValidator<RoleSearchRequest>
    {
        public RoleGetByDepartmentIdRequestValidator()
        {
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().Length(1, 50);
            //RuleFor(x => x.Status);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(RoleSearchRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new RoleGetByDepartmentIdRequestValidator().Validate(request);
            return validationResult;
        }
    }
}