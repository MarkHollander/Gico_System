using FluentValidation;
using Gico.SystemModels.Request.Banner;

namespace Gico.Cms.Validations
{
    public class BannerAddRequestValidate : AbstractValidator<BannerAddOrChangeRequest>
    {
        public BannerAddRequestValidate()
        {
            RuleFor(x => x.BannerName).NotNull().NotEmpty().Length(1, 1024);
            RuleFor(x => x.Status).IsInEnum();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(BannerAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new BannerAddRequestValidate().Validate(request);
            return validationResult;
        }
    }
}