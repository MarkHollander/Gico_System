using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupConditionGetRequestValidator : AbstractValidator<ProductGroupConditionGetRequest>
    {
        public ProductGroupConditionGetRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupConditionGetRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupConditionGetRequestValidator().Validate(request);
            return validationResult;

        }
    }
}