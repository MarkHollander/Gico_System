using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class BannersGetByMenuIdRequestValidate : AbstractValidator<BannerGetByMenuIdRequest>
    {
        public BannersGetByMenuIdRequestValidate()
        {
            RuleFor(x => x.MenuId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(BannerGetByMenuIdRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new BannersGetByMenuIdRequestValidate().Validate(request);
            return validationResult;
        }
    }
}