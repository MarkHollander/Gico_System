using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Gico.SystemModels.Request;
using Gico.SystemModels.Request.PageBuilder;

namespace Gico.Cms.Validations
{
    public class TemplateCheckCodeExistRequestValidate : AbstractValidator<TemplateCheckCodeExistRequest>
    {
        public TemplateCheckCodeExistRequestValidate()
        {
            RuleFor(x => x.TemplateId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Code).NotNull().NotEmpty().Length(1, 150);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(TemplateCheckCodeExistRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new TemplateCheckCodeExistRequestValidate().Validate(request);
            return validationResult;
        }
    }
}
