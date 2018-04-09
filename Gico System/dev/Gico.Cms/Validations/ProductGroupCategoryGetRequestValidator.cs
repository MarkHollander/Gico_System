using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupCategoryGetRequestValidator : AbstractValidator<ProductGroupCategoryGetRequest>
    {
        public ProductGroupCategoryGetRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupCategoryGetRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupCategoryGetRequestValidator().Validate(request);
            return validationResult;

        }
    }
}