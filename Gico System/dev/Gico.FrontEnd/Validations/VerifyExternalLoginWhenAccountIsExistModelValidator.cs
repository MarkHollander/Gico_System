using FluentValidation;
using Gico.Config;
using Gico.FrontEndModels.Models;

namespace Gico.FrontEnd.Validations
{
    public class VerifyExternalLoginWhenAccountIsExistModelValidator : AbstractValidator<VerifyExternalLoginWhenAccountIsExistModel>
    {
        public VerifyExternalLoginWhenAccountIsExistModelValidator()
        {

            RuleFor(x => x.VerifyId)
                .NotNull().WithMessage(ResourceKey.Verify_VerifyExternalLoginWhenAccountIsExist_VerifyId_NotNull)
                .NotEmpty().WithMessage(ResourceKey.Verify_VerifyExternalLoginWhenAccountIsExist_VerifyId_NotEmpty)
                .Length(3, 150).WithMessage(ResourceKey.Verify_VerifyExternalLoginWhenAccountIsExist_VerifyId_Length);
            RuleFor(x => x.VerifyCode)
                .NotNull().WithMessage(ResourceKey.Verify_VerifyExternalLoginWhenAccountIsExist_VerifyCode_NotNull)
                .NotEmpty().WithMessage(ResourceKey.Verify_VerifyExternalLoginWhenAccountIsExist_VerifyCode_NotEmpty);

        }

        public static FluentValidation.Results.ValidationResult ValidateModel(VerifyExternalLoginWhenAccountIsExistModel model)
        {
            FluentValidation.Results.ValidationResult validationResult = new VerifyExternalLoginWhenAccountIsExistModelValidator().Validate(model);
            return validationResult;
        }
    }
}