using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class MenuAddOrChangeRequestValidator : AbstractValidator<MenuAddOrChangeRequest>
    {
        public MenuAddOrChangeRequestValidator()
        {
            RuleFor(x => x.Menu).NotNull();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(MenuAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new MenuAddOrChangeRequestValidator().Validate(request);
            return validationResult;
        }
    }
}