using FluentValidation;
using Gico.SystemModels.Request.Banner;

namespace Gico.Cms.Validations
{
    public class BannerItemRemoveRequestValidate : AbstractValidator<BannerItemRemoveRequest>
    {
        public BannerItemRemoveRequestValidate()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.BannerId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(BannerItemRemoveRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new BannerItemRemoveRequestValidate().Validate(request);
            return validationResult;
        }
    }
}