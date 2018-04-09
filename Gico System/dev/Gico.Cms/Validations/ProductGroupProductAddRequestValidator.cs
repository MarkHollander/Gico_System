using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupProductAddRequestValidator : AbstractValidator<ProductGroupProductAddRequest>
    {
        public ProductGroupProductAddRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.ProductId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupProductAddRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupProductAddRequestValidator().Validate(request);
            return validationResult;

        }
    }
}