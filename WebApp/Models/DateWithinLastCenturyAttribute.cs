using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class DateWithinLastCenturyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                var centuryAgo = DateTime.Now.AddYears(-100);
                if (dateValue < centuryAgo)
                {
                    return new ValidationResult("La date ne peut pas être antérieure à il y a 100 ans.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
