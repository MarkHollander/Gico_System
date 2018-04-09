using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class DepartmentChangeRequestValidator : AbstractValidator<DepartmentChangeRequest>
    {
        public DepartmentChangeRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 150);
            RuleFor(x => x.Status);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(DepartmentChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new DepartmentChangeRequestValidator().Validate(request);
            return validationResult;
        }
    }
}