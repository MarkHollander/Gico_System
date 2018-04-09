using FluentValidation;
using Gico.OmsModels.Request;

namespace Gico.Oms.Validations
{
    public class GiftCodeCampaignAddRequestValidator : AbstractValidator<GiftCodeCampaignAddOrChangeRequest>
    {
        public GiftCodeCampaignAddRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 500);
            RuleFor(x => x.Notes).MaximumLength(2000);
            RuleFor(x => x.BeginDateValue.GetValueOrDefault())
                .LessThanOrEqualTo(x => x.EndDateValue.GetValueOrDefault());
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(GiftCodeCampaignAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new GiftCodeCampaignAddRequestValidator().Validate(request);
            return validationResult;
        }
    }
    public class GiftCodeCampaignChangeRequestValidator : AbstractValidator<GiftCodeCampaignAddOrChangeRequest>
    {
        public GiftCodeCampaignChangeRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 500);
            RuleFor(x => x.Notes).MaximumLength(2000);
            RuleFor(x => x.BeginDateValue.GetValueOrDefault())
                .LessThanOrEqualTo(x => x.EndDateValue.GetValueOrDefault());
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(GiftCodeCampaignAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new GiftCodeCampaignChangeRequestValidator().Validate(request);
            return validationResult;
        }
    }
    public class GiftCodeCampaignChangeStatusRequestValidator : AbstractValidator<GiftCodeCampaignChangeStatusRequest>
    {
        public GiftCodeCampaignChangeStatusRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Status).IsInEnum();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(GiftCodeCampaignChangeStatusRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new GiftCodeCampaignChangeStatusRequestValidator().Validate(request);
            return validationResult;
        }
    }
}