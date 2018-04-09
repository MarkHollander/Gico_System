using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class VendorAddRequestValidator : AbstractValidator<VendorAddOrChangeRequest>
    {
        public VendorAddRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().Length(3, 150);
            RuleFor(x => x.CompanyName).MaximumLength(1024);
            RuleFor(x => x.Fax).MaximumLength(50);
            RuleFor(x => x.Logo).MaximumLength(512);
            RuleFor(x => x.Website).MaximumLength(512);
            RuleFor(x => x.Phone).MaximumLength(50);
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 1024);
            RuleFor(x => x.Type).NotNull().IsInEnum();
            RuleFor(x => x.Status).NotNull().NotEmpty();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(VendorAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new VendorAddRequestValidator().Validate(request);
            return validationResult;
        }
    }

    public class VendorChangeRequestValidator : AbstractValidator<VendorAddOrChangeRequest>
    {
        public VendorChangeRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().Length(3, 150);
            RuleFor(x => x.CompanyName).MaximumLength(1024);
            RuleFor(x => x.Fax).MaximumLength(50);
            RuleFor(x => x.Logo).MaximumLength(512);
            RuleFor(x => x.Website).MaximumLength(512);
            RuleFor(x => x.Phone).MaximumLength(50);
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 1024);
            RuleFor(x => x.Type).NotNull().IsInEnum();
            RuleFor(x => x.Status).NotNull().NotEmpty();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(VendorAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new VendorChangeRequestValidator().Validate(request);
            return validationResult;
        }
    }
}