using FluentValidation;
using GaBon.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().Length(3, 50);
            RuleFor(x => x.Password).NotNull().NotEmpty().Length(5, 50);
            RuleFor(x => x.ConfirmPassword).Equal(p => p.Password);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(RegisterRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new RegisterRequestValidator().Validate(request);
            return validationResult;
        }
    }
}
