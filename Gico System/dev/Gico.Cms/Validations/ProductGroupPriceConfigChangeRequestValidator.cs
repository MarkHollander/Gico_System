using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupPriceConfigChangeRequestValidator : AbstractValidator<ProductGroupPriceConfigChangeRequest>
    {
        public ProductGroupPriceConfigChangeRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Prices).SetCollectionValidator(new ProductGroupPriceConfigRequestValidator());
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupPriceConfigChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupPriceConfigChangeRequestValidator().Validate(request);
            return validationResult;

        }
    }
}