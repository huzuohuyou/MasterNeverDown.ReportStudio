namespace AakStudio.Shell.UI.Showcase.ValidationRules;

public class PortValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is string portString && int.TryParse(portString, out var port))
        {
            if (port >= 1 && port <= 65535)
            {
                return ValidationResult.ValidResult;
            }
        }
        return new ValidationResult(false, "Invalid port number. Port must be between 1 and 65535.");
    }
}