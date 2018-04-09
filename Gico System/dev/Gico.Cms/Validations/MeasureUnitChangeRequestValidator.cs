using FluentValidation;
using Gico.SystemModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gico.Cms.Validations
{
    public class MeasureUnitChangeRequestValidator : AbstractValidator<MeasureUnitChangeRequest>
    {
        public MeasureUnitChangeRequestValidator()
        {
            RuleFor(x => x.UnitName).NotNull().NotEmpty().Length(3, 150);
            RuleFor(x => x.Ratio).NotNull().NotEmpty();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(MeasureUnitChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new MeasureUnitChangeRequestValidator().Validate(request);
            return validationResult;
        }
    }


}
