using System;
using System.Windows.Controls;
using System.Text.RegularExpressions;
namespace TourPlanner.Validation;

public class TourLocationValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
    {
        //define regex for a string that only allows letters and numbers
        Regex regex = new Regex(@"^[a-zA-Z0-9\s,.'-]*$");

        // Check if the value is null or empty
        if (string.IsNullOrEmpty(value as string))
        {
            return new ValidationResult(false, "Field cannot be empty.");
        }
        // Check if the value is a valid address string
        else if (!regex.IsMatch(value as string))
        {
            return new ValidationResult(false, "Field can only contain letters, numbers, spaces, commas, periods, dashes, and apostrophes.");
        }
        else
        {
            return ValidationResult.ValidResult;
        }
    }
    
}