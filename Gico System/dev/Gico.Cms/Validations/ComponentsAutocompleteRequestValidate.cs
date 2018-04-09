using FluentValidation;
using Gico.SystemModels.Request.PageBuilder;

namespace Gico.Cms.Validations
{
    public class ComponentsAutocompleteRequestValidate : AbstractValidator<ComponentsAutocompleteRequest>
    {
        public ComponentsAutocompleteRequestValidate()
        {
            RuleFor(x => x.ComponentType).IsInEnum();
            RuleFor(x => x.Tearm).MaximumLength(1024);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ComponentsAutocompleteRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ComponentsAutocompleteRequestValidate().Validate(request);
            return validationResult;
        }
    }
}