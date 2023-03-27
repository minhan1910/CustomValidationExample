using System.ComponentModel.DataAnnotations;

namespace ModelValidationExample.CustomValidators
{
    public class MinimumYearValidatorAttribute : ValidationAttribute
    {
        public int MinimumYear { get; set; }
        
        private static string DefaultErrorMessage { get; } = "Year should not be less than {0}";

        // parameterless constructor
        public MinimumYearValidatorAttribute() 
        {
            MinimumYear = 2000;
        }

        public MinimumYearValidatorAttribute(int minimumYear)
        {
            MinimumYear= minimumYear;   
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return null;

            string displayName = validationContext.DisplayName;
            DateTime date = (DateTime)value;
            
            if (date.Year <= MinimumYear)
            {
                // received from ErrorMessage Attribute Model 
                return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, displayName, MinimumYear));
            }

            return ValidationResult.Success;            
        }

        // Default Error Message (Can custom)
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name);
        }
    }
}
