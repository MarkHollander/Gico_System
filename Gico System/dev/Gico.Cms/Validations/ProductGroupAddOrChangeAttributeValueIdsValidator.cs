using FluentValidation;

namespace Gico.Cms.Validations
{
    public class ProductGroupAddOrChangeAttributeValueIdsValidator : AbstractValidator<string>
    {
        public ProductGroupAddOrChangeAttributeValueIdsValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().Length(1, 50);
        }
    }
}