using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupVendorAddRequestValidator : AbstractValidator<ProductGroupVendorAddRequest>
    {
        public ProductGroupVendorAddRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.VendorId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupVendorAddRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupVendorAddRequestValidator().Validate(request);
            return validationResult;

        }
    }
}