using FluentValidation;
using Gico.SystemModels.Models;

namespace Gico.Cms.Validations
{
    public class MenuModelAddModelValidator : AbstractValidator<MenuModel>
    {
        public MenuModelAddModelValidator()
        {
            RuleFor(x => x.LanguageId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 500);
            RuleFor(x => x.Position).IsInEnum();
            RuleFor(x => x.Type).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Status).InclusiveBetween(0, 1);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(MenuModel request)
        {
            FluentValidation.Results.ValidationResult validationResult = new MenuModelAddModelValidator().Validate(request);
            return validationResult;
        }
    }
}