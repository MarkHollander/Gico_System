using FluentValidation;
using Gico.SystemModels.Models;
using Gico.SystemModels.Request;

namespace Gico.Cms.Validations
{
    public class ShardingConfigGetRequestValidator : AbstractValidator<ShardingConfigGetRequest>
    {
        public ShardingConfigGetRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ShardingConfigGetRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ShardingConfigGetRequestValidator().Validate(request);
            return validationResult;
        }
    }
    public class ShardingConfigAddRequestValidator : AbstractValidator<ShardingConfigAddOrChangeRequest>
    {
        public ShardingConfigAddRequestValidator()
        {
            RuleFor(x => x.ShardingConfig).NotNull();
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ShardingConfigAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ShardingConfigAddRequestValidator().Validate(request);
            return validationResult;
        }
    }
    public class ShardingConfigAddModelValidator : AbstractValidator<ShardingConfigModel>
    {
        public ShardingConfigAddModelValidator()
        {
            //RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            //RuleFor(x => x.Status).GreaterThan(0);
            //RuleFor(x => x.HostName).NotNull().NotEmpty().Length(1, 50);
            //RuleFor(x => x.DatabaseName).NotNull().NotEmpty().Length(1, 50);
            //RuleFor(x => x.Uid).NotNull().NotEmpty().Length(1, 50);
            //RuleFor(x => x.Pwd).NotNull().NotEmpty().Length(1, 50);
            //RuleFor(x => x.Type).GreaterThan(0);
            //RuleFor(x => x.ShardGroup).GreaterThan(0);

        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ShardingConfigModel request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ShardingConfigAddModelValidator().Validate(request);
            return validationResult;
        }
    }
}