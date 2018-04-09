using FluentValidation;
using GaBon.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().Length(3, 50);
            RuleFor(x => x.Password).NotNull().NotEmpty().Length(5, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(LoginRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new LoginRequestValidator().Validate(request);
            return validationResult;
        }
    }
}