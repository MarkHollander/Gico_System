using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupAddOrChangeAttributeRequestValidator : AbstractValidator<ProductGroupAddOrChangeAttributeRequest>
    {
        public ProductGroupAddOrChangeAttributeRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.AttributeId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.AttributeValueIds).NotNull().SetCollectionValidator(new ProductGroupAddOrChangeAttributeValueIdsValidator());
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupAddOrChangeAttributeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupAddOrChangeAttributeRequestValidator().Validate(request);
            return validationResult;

        }
    }
}