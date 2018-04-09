using FluentValidation;
using Gico.Config;
using Gico.FrontEndModels.Models;

namespace Gico.FrontEnd.Validations
{
    public class ExternalLoginConfirmModelValidator : AbstractValidator<ExternalLoginConfirmViewModel>
    {
        public ExternalLoginConfirmModelValidator()
        {

            RuleFor(x => x.Email)
                .NotNull().WithMessage(ResourceKey.Account_Register_EmailOrMobile_NotNull)
                .NotEmpty().WithMessage(ResourceKey.Account_Register_EmailOrMobile_NotEmpty)
                .Length(3, 150).WithMessage(ResourceKey.Account_Register_EmailOrMobile_Length);
            RuleFor(x => x.LoginProvider).IsInEnum().WithMessage(ResourceKey.Account_Register_LoginProvider_IsInEnum);

        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ExternalLoginConfirmViewModel model)
        {
            FluentValidation.Results.ValidationResult validationResult = new ExternalLoginConfirmModelValidator().Validate(model);
            return validationResult;
        }
    }
}