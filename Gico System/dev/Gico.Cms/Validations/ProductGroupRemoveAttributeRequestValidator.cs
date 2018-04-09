using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupRemoveAttributeRequestValidator : AbstractValidator<ProductGroupRemoveAttributeRequest>
    {
        public ProductGroupRemoveAttributeRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.AttributeId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupRemoveAttributeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupRemoveAttributeRequestValidator().Validate(request);
            return validationResult;

        }
    }
}