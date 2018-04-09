using FluentValidation;
using Gico.SystemModels.Request.Banner;

namespace Gico.Cms.Validations
{
    public class BannerRemoveRequestValidate : AbstractValidator<BannerRemoveRequest>
    {
        public BannerRemoveRequestValidate()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(BannerRemoveRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new BannerRemoveRequestValidate().Validate(request);
            return validationResult;
        }
    }
}