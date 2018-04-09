using FluentValidation;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class AttrCategoryRequestValidate
    {
    }
    public class AttrCategoryAddRequestValidator : AbstractValidator<AttrCategoryAddRequest>
    {
        public AttrCategoryAddRequestValidator()
        {
            RuleFor(x => x.AttrCategory).NotNull();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(AttrCategoryAddRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new AttrCategoryAddRequestValidator().Validate(request);
            return validationResult;
        }

    }

    public class AttrCategoryChangeRequestValidator : AbstractValidator<AttrCategoryChangeRequest>
    {
        public AttrCategoryChangeRequestValidator()
        {
            RuleFor(x => x.AttrCategory).NotNull();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(AttrCategoryChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new AttrCategoryChangeRequestValidator().Validate(request);
            return validationResult;
        }

    }
    public class AttrCategoryRemoveRequestValidator : AbstractValidator<AttrCategoryRemoveRequest>
    {
        public AttrCategoryRemoveRequestValidator()
        {
            RuleFor(x => x.AttributeId).NotNull();
            RuleFor(x => x.CategoryId).NotNull();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(AttrCategoryRemoveRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new AttrCategoryRemoveRequestValidator().Validate(request);
            return validationResult;
        }

    }

    public class AttrCategoryModelAddModelValidator : AbstractValidator<AttrCategoryModel>
    {
        public AttrCategoryModelAddModelValidator()
        {


            RuleFor(x => x.AttributeId).NotNull().NotEmpty();
            RuleFor(x => x.CategoryId).NotNull().NotEmpty();
            RuleFor(x => x.DisplayOrder).NotNull().NotEmpty();




        }

        public static FluentValidation.Results.ValidationResult ValidateModel(AttrCategoryModel request)
        {
            FluentValidation.Results.ValidationResult validationResult = new AttrCategoryModelAddModelValidator().Validate(request);
            return validationResult;

        }
    }
}
