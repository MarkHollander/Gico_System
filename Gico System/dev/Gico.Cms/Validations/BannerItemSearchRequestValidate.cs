using FluentValidation;
using Gico.SystemModels.Request.Banner;

namespace Gico.Cms.Validations
{
    public class BannerItemSearchRequestValidate : AbstractValidator<BannerItemSearchRequest>
    {
        public BannerItemSearchRequestValidate()
        {
            RuleFor(x => x.BannerId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(BannerItemSearchRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new BannerItemSearchRequestValidate().Validate(request);
            return validationResult;
        }
    }
}