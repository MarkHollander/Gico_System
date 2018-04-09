using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupGetRequestValidator : AbstractValidator<ProductGroupGetRequest>
    {
        public ProductGroupGetRequestValidator()
        {
            //RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupGetRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupGetRequestValidator().Validate(request);
            return validationResult;

        }
    }
}