namespace AakStudio.Shell.UI.Showcase.ValidationRules;

public class StringLengthValidationRule : ValidationRule
{
    public int MinLength { get; set; } = 0;
    public int MaxLength { get; set; } = int.MaxValue;

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is string str)
        {
            if (str.Length < MinLength)
            {
                return new ValidationResult(false, $"String length must be at least {MinLength} characters.");
            }
            if (str.Length > MaxLength)
            {
                return new ValidationResult(false, $"String length cannot exceed {MaxLength} characters.");
            }
            return ValidationResult.ValidResult;
        }
        return new ValidationResult(false, "Invalid input.");
    }
}