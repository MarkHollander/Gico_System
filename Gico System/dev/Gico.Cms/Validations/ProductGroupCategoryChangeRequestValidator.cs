using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupCategoryChangeRequestValidator : AbstractValidator<ProductGroupCategoryChangeRequest>
    {
        public ProductGroupCategoryChangeRequestValidator()
        {
            RuleFor(x => x.ProductGroupId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupCategoryChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupCategoryChangeRequestValidator().Validate(request);
            return validationResult;

        }
    }
}