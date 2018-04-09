using FluentValidation;
using Gico.SystemModels.Request.Banner;

namespace Gico.Cms.Validations
{
    public class BannerItemGetRequestValidate : AbstractValidator<BannerItemGetRequest>
    {
        public BannerItemGetRequestValidate()
        {
            RuleFor(x => x.BannerId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(BannerItemGetRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new BannerItemGetRequestValidate().Validate(request);
            return validationResult;
        }
    }
}