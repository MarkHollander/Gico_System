using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class ProductAttributeRequestValidator : AbstractValidator<ProductAttributeCrudRequest>
    {
        public ProductAttributeRequestValidator()
        {
            RuleFor(x => x.Name);
            RuleFor(x => x.Status);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductAttributeCrudRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductAttributeRequestValidator().Validate(request);
            return validationResult;
        }
    }

    public class ProductAttributeValueRequestValidator : AbstractValidator<ProductAttributeValueCrudRequest>
    {
        public ProductAttributeValueRequestValidator()
        {
            RuleFor(x => x.AttributeId);
            RuleFor(x => x.Value);
            RuleFor(x => x.UnitId);
            RuleFor(x => x.Order);
            RuleFor(x => x.Status);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductAttributeValueCrudRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductAttributeValueRequestValidator().Validate(request);
            return validationResult;
        }
    }
}