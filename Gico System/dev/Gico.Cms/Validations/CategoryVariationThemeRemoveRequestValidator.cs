using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class CategoryVariationThemeRemoveRequestValidator : AbstractValidator<Category_VariationTheme_Mapping_RemoveRequest>
    {

        public CategoryVariationThemeRemoveRequestValidator()
        {
            RuleFor(x => x.CategoryId).MaximumLength(50);

        }
        public static FluentValidation.Results.ValidationResult ValidateModel(Category_VariationTheme_Mapping_RemoveRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new CategoryVariationThemeRemoveRequestValidator().Validate(request);
            return validationResult;
        }
    }
 
}
