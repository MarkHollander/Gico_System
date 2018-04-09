using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class CategoryVariationThemeAddRequestValidator : AbstractValidator<Category_VariationTheme_MappingAddRequest>
    {
        public CategoryVariationThemeAddRequestValidator()
        {
            RuleFor(x => x.Category_VariationTheme_Mapping.CategoryId).MaximumLength(50);
            
        }
        public static FluentValidation.Results.ValidationResult ValidateModel(Category_VariationTheme_MappingAddRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new CategoryVariationThemeAddRequestValidator().Validate(request);
            return validationResult;
        }
    }
}
