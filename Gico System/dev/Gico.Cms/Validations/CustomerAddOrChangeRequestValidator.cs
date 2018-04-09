using FluentValidation;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class CustomerAddRequestValidator : AbstractValidator<CustomerAddOrChangeRequest>
    {
        public CustomerAddRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().Length(3, 150);
            RuleFor(x => x.AdminComment).MaximumLength(2000);
            RuleFor(x => x.BillingAddressId).MaximumLength(50);
            RuleFor(x => x.ShippingAddressId).MaximumLength(50);
            RuleFor(x => x.Password).NotNull().NotEmpty().Length(6, 150);
            RuleFor(x => x.PhoneNumber).MaximumLength(50);
            RuleFor(x => x.TwoFactorEnabled).IsInEnum();
            RuleFor(x => x.FullName).NotNull().NotEmpty().Length(1, 150);
            RuleFor(x => x.Gender).IsInEnum();
            RuleFor(x => x.Birthday).Length(8, 10);
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Status).NotNull().NotEmpty();
            RuleFor(x => x.LanguageId).NotNull().NotEmpty().Length(1, 50);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(CustomerAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new CustomerAddRequestValidator().Validate(request);
            return validationResult;
        }
    }
}