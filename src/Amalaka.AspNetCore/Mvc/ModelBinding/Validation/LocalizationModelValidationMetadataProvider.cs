using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class LocalizationModelValidationMetadataProvider : IValidationMetadataProvider
{
    public void CreateValidationMetadata(ValidationMetadataProviderContext context)
    {
        var requiredAttribute = (RequiredAttribute?)
            (context.PropertyAttributes?.FirstOrDefault(f => f is RequiredAttribute) ??
             context.ParameterAttributes?.FirstOrDefault(f => f is RequiredAttribute));
        if (requiredAttribute != null && string.IsNullOrWhiteSpace(requiredAttribute.ErrorMessage))
        {
            var descriptionAttribute = (DescriptionAttribute?)context.PropertyAttributes?.FirstOrDefault(f => f is DescriptionAttribute);
            requiredAttribute.ErrorMessage = $"{(descriptionAttribute?.Description) ?? "{0}"}不能为空";
        }
    }
}