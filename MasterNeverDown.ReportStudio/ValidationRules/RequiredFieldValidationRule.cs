namespace AakStudio.Shell.UI.Showcase.ValidationRules;

public class RequiredFieldValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        return string.IsNullOrWhiteSpace(value as string) ? new ValidationResult(false, "This field is required.") : ValidationResult.ValidResult;
    }
}