using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupAttributeConfigGetRequestValidator : AbstractValidator<ProductGroupAttributeConfigGetRequest>
    {
        public ProductGroupAttributeConfigGetRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.AttributeId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupAttributeConfigGetRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupAttributeConfigGetRequestValidator().Validate(request);
            return validationResult;

        }
    }
}