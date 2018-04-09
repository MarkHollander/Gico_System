using FluentValidation;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;
using Gico.Common;

namespace Gico.Cms.Validations
{
    public class CategoryRequestValidate
    {
    }

    public class CategoryAddOrChangeRequestValidator : AbstractValidator<CategoryAddOrChangeRequest>
    {
        public CategoryAddOrChangeRequestValidator()
        {
            RuleFor(x => x.Category).NotNull();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(CategoryAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new CategoryAddOrChangeRequestValidator().Validate(request);
            return validationResult;
        }
    }
    public class CategoryModelAddModelValidator : AbstractValidator<CategoryModel>
    {
        public CategoryModelAddModelValidator()
        {
            RuleFor(x => x.LanguageId).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 500);
            RuleFor(x => x.Status).IsInEnum();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(CategoryModel request)
        {
            FluentValidation.Results.ValidationResult validationResult = new CategoryModelAddModelValidator().Validate(request);
            return validationResult;

        }
    }
}
