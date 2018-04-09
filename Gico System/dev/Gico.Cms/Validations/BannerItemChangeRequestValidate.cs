using FluentValidation;
using Gico.SystemModels.Request.Banner;

namespace Gico.Cms.Validations
{
    public class BannerItemChangeRequestValidate : AbstractValidator<BannerItemAddOrChangeRequest>
    {
        public BannerItemChangeRequestValidate()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.BannerItemName).NotNull().NotEmpty().Length(1, 1024);
            RuleFor(x => x.BannerId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.TargetUrl).NotNull().NotEmpty().Length(1, 2048);
            RuleFor(x => x.ImageUrl).NotNull().NotEmpty().Length(1, 2048);
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.StartDateUtc).LessThanOrEqualTo(p => p.EndDateUtc);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(BannerItemAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new BannerItemChangeRequestValidate().Validate(request);
            return validationResult;
        }
    }
}