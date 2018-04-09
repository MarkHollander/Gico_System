using FluentValidation.Results;
using Gico.FrontEndModels.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gico.FrontEnd.Validations
{
    public static class ValidationExtension
    {
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState, string prefix, PageModel pageModel)
        {
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    string key = string.IsNullOrEmpty(prefix) ? error.PropertyName : $"{prefix}.{error.PropertyName}";
                    string message = pageModel.T(error.ErrorMessage);
                    modelState.AddModelError(key, message);
                }
            }
        }
    }
}