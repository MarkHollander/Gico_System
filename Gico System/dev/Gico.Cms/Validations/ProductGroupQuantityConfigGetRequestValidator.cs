using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupQuantityConfigGetRequestValidator : AbstractValidator<ProductGroupQuantityConfigGetRequest>
    {
        public ProductGroupQuantityConfigGetRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupQuantityConfigGetRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupQuantityConfigGetRequestValidator().Validate(request);
            return validationResult;

        }
    }
}