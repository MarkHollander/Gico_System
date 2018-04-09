using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupPriceConfigRequestValidator : AbstractValidator<ProductGroupPriceConfigModel>
    {
        public ProductGroupPriceConfigRequestValidator()
        {
            RuleFor(x => x.MinPrice).LessThanOrEqualTo(x => x.MaxPrice);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupPriceConfigModel request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupPriceConfigRequestValidator().Validate(request);
            return validationResult;

        }
    }
}