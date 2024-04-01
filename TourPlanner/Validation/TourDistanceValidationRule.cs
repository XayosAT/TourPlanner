using System;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace TourPlanner.Validation;

public class TourDistanceValidationRule :ValidationRule
{
    public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
    {
        //define regex for a string that only allows numbers and a dot
        Regex regex = new Regex("^[0-9.]*$");
        
        //check if the value is null or empty
        if (string.IsNullOrEmpty(value as string))
        {
            return new ValidationResult(false, "Field cannot be empty.");
        }
        //check if the value is a valid string
        else if (!regex.IsMatch(value as string))
        {
            return new ValidationResult(false, "Field can only contain numbers and a dot.");
        }
        else
        {
            return ValidationResult.ValidResult;
        }
    }
}