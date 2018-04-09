using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class ManufacturerManagementAddRequestValidator:AbstractValidator<ManufacturerManagementAddOrChangeRequest>
    {
        public ManufacturerManagementAddRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 1024);
            RuleFor(x => x.Logo).MaximumLength(512);
            RuleFor(x => x.Description).MaximumLength(2048);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ManufacturerManagementAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ManufacturerManagementAddRequestValidator().Validate(request);
            return validationResult;
        }
    }

    public class ManufacturerManagementChangeRequestValidator : AbstractValidator<ManufacturerManagementAddOrChangeRequest>
    {
        public ManufacturerManagementChangeRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 1024);
            RuleFor(x => x.Logo).MaximumLength(512);
            RuleFor(x => x.Description).MaximumLength(2048);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ManufacturerManagementAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ManufacturerManagementChangeRequestValidator().Validate(request);
            return validationResult;
        }
    }
}
