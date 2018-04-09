using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class MenuGetRequestValidator : AbstractValidator<MenuGetRequest>
    {
        public MenuGetRequestValidator()
        {
            RuleFor(x => x.LanguageId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Position).IsInEnum();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(MenuGetRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new MenuGetRequestValidator().Validate(request);
            return validationResult;
        }
    }
}