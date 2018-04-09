using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class MenuBannerMappingAddRequestValidate : AbstractValidator<MenuBannerMappingAddRequest>
    {
        public MenuBannerMappingAddRequestValidate()
        {
            RuleFor(x => x.MenuId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.BannerId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(MenuBannerMappingAddRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new MenuBannerMappingAddRequestValidate().Validate(request);
            return validationResult;
        }
    }
}