using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class ProvinceUpdateRequestValidator : AbstractValidator<LocationUpdateRequest>
    {
        public ProvinceUpdateRequestValidator()
        {
            RuleFor(x => x.ProvinceName).NotNull();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(LocationUpdateRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProvinceUpdateRequestValidator().Validate(request);
            return validationResult;
        }
    }

    public class DistrictRequestValidator : AbstractValidator<LocationUpdateRequest>
    {
        public DistrictRequestValidator()
        {
            RuleFor(x => x.DistrictName);
            RuleFor(x => x.DistrictNameEN);
            RuleFor(x => x.ProvinceId);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(LocationUpdateRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new DistrictRequestValidator().Validate(request);
            return validationResult;
        }
    }

    public class WardRequestValidator : AbstractValidator<LocationUpdateRequest>
    {
        public WardRequestValidator()
        {
            RuleFor(x => x.WardName);
            RuleFor(x => x.WardNameEN);
            RuleFor(x => x.DistrictId);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(LocationUpdateRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new WardRequestValidator().Validate(request);
            return validationResult;
        }
    }
}
