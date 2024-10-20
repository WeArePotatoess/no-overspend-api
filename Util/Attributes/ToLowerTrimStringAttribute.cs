using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace TaxNet_Common.Attributes
{
    public class ToLowerTrimStringAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext ctx)
        {
            if (value is string stringValue)
            {
                var trimmedValue = stringValue.Trim().ToLower();
                var property = ctx.ObjectType.GetProperty(ctx.MemberName!);
                if (property != null)
                {
                    property.SetValue(ctx.ObjectInstance, trimmedValue);
                }
            }
            return ValidationResult.Success!;
        }
    }
}
