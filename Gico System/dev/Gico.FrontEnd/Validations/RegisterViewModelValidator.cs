using FluentValidation;
using Gico.Config;
using Gico.FrontEndModels.Models;

namespace Gico.FrontEnd.Validations
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.FullName)
                .NotNull().WithMessage(ResourceKey.Account_Register_FullName_NotNull)
                .NotEmpty().WithMessage(ResourceKey.Account_Register_FullName_NotEmpty)
                .Length(2, 150).WithMessage((ResourceKey.Account_Register_FullName_Length));

            RuleFor(x => x.EmailOrMobile)
                .NotNull().WithMessage(ResourceKey.Account_Register_EmailOrMobile_NotNull)
                .NotEmpty().WithMessage(ResourceKey.Account_Register_EmailOrMobile_NotEmpty)
                .Length(3, 150).WithMessage(ResourceKey.Account_Register_EmailOrMobile_Length);

            RuleFor(x => x.Password)
                .NotNull().WithMessage(ResourceKey.Account_Register_Password_NotNull)
                .NotEmpty().WithMessage(ResourceKey.Account_Register_Password_NotEmpty)
                .Length(6, 150).WithMessage(ResourceKey.Account_Register_Password_Length);
            RuleFor(x => x.ConfirmPassword)
                .Equal(p => p.Password).WithMessage(ResourceKey.Account_Register_ConfirmPassword_Equal);
            RuleFor(x => x.Gender).IsInEnum().WithMessage(ResourceKey.Account_Register_Gender_IsInEnum);
            RuleFor(x => x.BirthdayValue)
                .NotNull().WithMessage(ResourceKey.Account_Register_BirthdayValue_NotNull)
                .NotEmpty().WithMessage(ResourceKey.Account_Register_BirthdayValue_NotEmpty);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(RegisterViewModel model)
        {
            FluentValidation.Results.ValidationResult validationResult = new RegisterViewModelValidator().Validate(model);
            return validationResult;
        }
    }

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull().WithMessage(ResourceKey.Account_Login_Username_NotNull)
                .NotEmpty().WithMessage(ResourceKey.Account_Login_Username_NotEmpty)
                .Length(3, 150).WithMessage((ResourceKey.Account_Login_Username_Length));

            RuleFor(x => x.Password)
                .NotNull().WithMessage(ResourceKey.Account_Login_Password_NotNull)
                .NotEmpty().WithMessage(ResourceKey.Account_Login_Password_NotEmpty)
                .Length(3, 150).WithMessage(ResourceKey.Account_Login_Password_Length);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(LoginViewModel model)
        {
            FluentValidation.Results.ValidationResult validationResult = new LoginViewModelValidator().Validate(model);
            return validationResult;
        }
    }
}
