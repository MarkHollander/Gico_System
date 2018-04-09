using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class DepartmentAddRequestValidator : AbstractValidator<DepartmentAddRequest>
    {
        public DepartmentAddRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 150);
            RuleFor(x => x.Status);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(DepartmentAddRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new DepartmentAddRequestValidator().Validate(request);
            return validationResult;
        }
    }
}