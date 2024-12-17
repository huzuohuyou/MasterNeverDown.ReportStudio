using System.Net;

namespace AakStudio.Shell.UI.Showcase.ValidationRules;

public class IpValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is string ipString && IPAddress.TryParse(ipString, out var ok))
        {
            return ValidationResult.ValidResult;
        }
        return new ValidationResult(false, "Invalid IP address.");
    }
}