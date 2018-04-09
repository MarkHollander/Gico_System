using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupPriceConfigGetRequestValidator : AbstractValidator<ProductGroupPriceConfigGetRequest>
    {
        public ProductGroupPriceConfigGetRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupPriceConfigGetRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupPriceConfigGetRequestValidator().Validate(request);
            return validationResult;

        }
    }
}