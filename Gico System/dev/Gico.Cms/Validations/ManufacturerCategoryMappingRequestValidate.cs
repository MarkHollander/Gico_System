using FluentValidation;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class ManufacturerCategoryMappingRequestValidate
    {
    }
    public class ManufacturerCategoryMappingRemoveRequestValidator : AbstractValidator<ManufacturerCategoryMappingRemoveRequest>
    {
        public ManufacturerCategoryMappingRemoveRequestValidator()
        {
            RuleFor(x => x.CategoryId).NotNull();
            RuleFor(x => x.ManufacturerId).NotNull();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ManufacturerCategoryMappingRemoveRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ManufacturerCategoryMappingRemoveRequestValidator().Validate(request);
            return validationResult;
        }

    }
}
