using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupQuantityConfigRequestValidator : AbstractValidator<ProductGroupQuantityConfigModel>
    {
        public ProductGroupQuantityConfigRequestValidator()
        {
            RuleFor(x => x.MinQuantity).LessThanOrEqualTo(x => x.MaxQuantity);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupQuantityConfigModel request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupQuantityConfigRequestValidator().Validate(request);
            return validationResult;

        }
    }
}