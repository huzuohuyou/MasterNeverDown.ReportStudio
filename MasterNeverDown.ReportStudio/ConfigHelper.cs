using System.Configuration;

public static class ConfigHelper
{
    public static void UpdateAppConfig(string key, string value)
    {
        var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        var settings = configFile.AppSettings.Settings;

        if (settings[key] == null)
        {
            settings.Add(key, value);
        }
        else
        {
            settings[key].Value = value;
        }

        configFile.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
    }
    
    
    public static string GetSelectedLanguage()
    {
        var selectedLanguage = ConfigurationManager.AppSettings["SelectedLanguage"];
        return selectedLanguage ; // Default to "en-US" if the key is not found
    }
}