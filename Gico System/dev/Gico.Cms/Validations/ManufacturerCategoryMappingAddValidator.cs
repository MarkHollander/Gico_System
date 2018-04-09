using FluentValidation;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class ManufacturerCategoryMappingAddValidator
    {
    }
    public class ManufacturerCategoryMappingAddRequestValidator : AbstractValidator<ManufacturerMappingAddRequest>
    {
        public ManufacturerCategoryMappingAddRequestValidator()
        {
            RuleFor(x => x.CategoryId).NotNull();
            RuleFor(x => x.ManufacturerId).NotNull();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ManufacturerMappingAddRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ManufacturerCategoryMappingAddRequestValidator().Validate(request);
            return validationResult;
        }

    }
}
