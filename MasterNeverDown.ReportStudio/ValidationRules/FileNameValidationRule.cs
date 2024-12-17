namespace AakStudio.Shell.UI.Showcase.ValidationRules;

public class FileNameValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is string fileName && !string.IsNullOrWhiteSpace(fileName))
        {
            try
            {
                // Check for invalid characters in the file name
                if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    return new ValidationResult(false, "Invalid file name.");
                }
                return ValidationResult.ValidResult;
            }
            catch
            {
                return new ValidationResult(false, "Invalid file name.");
            }
        }
        return new ValidationResult(false, "File name cannot be empty.");
    }
}