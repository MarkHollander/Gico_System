using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupManufacturerAddRequestValidator : AbstractValidator<ProductGroupManufacturerAddRequest>
    {
        public ProductGroupManufacturerAddRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.ManufacturerId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupManufacturerAddRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupManufacturerAddRequestValidator().Validate(request);
            return validationResult;

        }
    }
}