using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupQuantityConfigChangeRequestValidator : AbstractValidator<ProductGroupQuantityConfigChangeRequest>
    {
        public ProductGroupQuantityConfigChangeRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Quantities).SetCollectionValidator(new ProductGroupQuantityConfigRequestValidator());
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupQuantityConfigChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupQuantityConfigChangeRequestValidator().Validate(request);
            return validationResult;

        }
    }
}