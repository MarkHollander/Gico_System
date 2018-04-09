﻿using FluentValidation;
using Gico.SystemModels.Request.ProductGroup;

namespace Gico.Cms.Validations
{
    public class ProductGroupAddRequestValidator : AbstractValidator<ProductGroupAddOrChangeRequest>
    {
        public ProductGroupAddRequestValidator()
        {
            //RuleFor(x => x.Id).NotNull().NotEmpty().Length(1, 50);
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 256);
            RuleFor(x => x.Status).IsInEnum();
            RuleFor(x => x.Description).MaximumLength(1024);
        }

        public static FluentValidation.Results.ValidationResult ValidateModel(ProductGroupAddOrChangeRequest request)
        {
            FluentValidation.Results.ValidationResult validationResult = new ProductGroupAddRequestValidator().Validate(request);
            return validationResult;

        }
    }
}