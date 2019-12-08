using System;
using System.ComponentModel.DataAnnotations;

namespace PonyWebChallenge.Extensions
{
    public class EnumAttributeValidationExtension
    {
        public class ValidateDifficultyAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                Type enumType = value.GetType();
                bool valid = Enum.IsDefined(enumType, value);
                if (!valid)
                {
                    return new ValidationResult(String.Format("{0} is not a valid value for type {1}", value, enumType.Name));
                }
                return ValidationResult.Success;
            }
        }

    }
}