using FluentValidation;
using Gico.SystemModels.Request.Banner;

namespace Gico.Cms.Validations
{
    public class BannerChangeRequestValidate : AbstractValidator<BannerAddOrChangeRequest>
    {
        public BannerChangeRequestValidate()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.BannerName).NotNull().NotEmpty().Length(1, 1024);
            RuleFor(x => x.Status).IsInEnum();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(BannerAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new BannerChangeRequestValidate().Validate(request);
            return validationResult;
        }
    }
}