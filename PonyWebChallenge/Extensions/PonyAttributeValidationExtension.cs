using PonyWebChallenge.Helper;
using System;
using System.ComponentModel.DataAnnotations;

namespace PonyWebChallenge.Extensions
{
    public class PonyAttributeValidationExtension
    {
        public class ValidatePonyAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                bool valid = PoniesHelper.ContainsPony(value.ToString());
                if (!valid)
                {
                    return new ValidationResult(String.Format("{0} is not a valid value for Pony type", value));
                }
                return ValidationResult.Success;
            }
        }

    }
}