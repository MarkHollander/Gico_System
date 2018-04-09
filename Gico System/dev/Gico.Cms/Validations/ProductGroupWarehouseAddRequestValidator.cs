using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupWarehouseAddRequestValidator : AbstractValidator<ProductGroupWarehouseAddRequest>
    {
        public ProductGroupWarehouseAddRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.WarehouseId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupWarehouseAddRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupWarehouseAddRequestValidator().Validate(request);
            return validationResult;

        }
    }
}