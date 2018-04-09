using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupAttributesConfigGetRequestValidator : AbstractValidator<ProductGroupAttributesConfigGetRequest>
    {
        public ProductGroupAttributesConfigGetRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupAttributesConfigGetRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupAttributesConfigGetRequestValidator().Validate(request);
            return validationResult;

        }
    }
}