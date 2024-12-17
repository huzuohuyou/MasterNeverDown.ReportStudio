using System;
using System.Reflection;
using MasterNeverDown.ReportStudio.ToolBar.LangRes;

public static class LangResourceHelper
{
    public static object GetLangResourceValue(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        Type langResourceType = typeof(LangResource);
        PropertyInfo property = langResourceType.GetProperty(name, BindingFlags.Public | BindingFlags.Static);

        if (property == null)
        {
            throw new ArgumentException($"Property '{name}' not found in LangResource.");
        }

        return property.GetValue(null);
    }
}