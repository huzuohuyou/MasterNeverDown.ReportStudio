

using CommunityToolkit.Mvvm.Input;
using MasterNeverDown.ReportStudio.Model.LangRes;
using CheckBox = System.Windows.Controls.CheckBox;
using TextBox = System.Windows.Controls.TextBox;

namespace AakStudio.Shell.UI.Showcase.ViewModels;

public partial class PreviewTemplateViewModel : ViewModelBase
{
    
    private ReoGridControl grid = null!;

    private readonly DataSourceContext? _dataSourceContext =
        App.ServiceProvider.GetService(typeof(DataSourceContext)) as DataSourceContext;

    public string? TemplateName
    {
        get => _templateName;
        set
        {
            _templateName = value;
            OnPropertyChanged(nameof(TemplateName));
        }
    }

    private string? _templateName;
    
    [RelayCommand]
    private void OnLoadedWindow(object sender)
    {
        grid = (ReoGridControl)sender;
        grid.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
    }
   
    [RelayCommand]
    private void OnLoadedTemplateName(object sender)
    {
        TemplateName = ((TextBlock)sender).Text;
        LoadParameters();
        LoadFile();
    }

    private StackPanel parameterContainer = null!;
    [RelayCommand]
    private void OnLoadedParameterContainer(object sender)
    {
        parameterContainer = (StackPanel)sender;
        // LoadFile();
        
    }

    private readonly List<Parameter> _parameters = [];
    
    private void LoadParameters()
    {
        if (string.IsNullOrWhiteSpace(TemplateName))
        {
            System.Windows.Forms.MessageBox.Show(LangResource.Template_cant_null, LangResource.caption,MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_dataSourceContext == null)
        {
            System.Windows.Forms.MessageBox.Show(LangResource.datasource_cant_null, LangResource.caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        _parameters.Clear();
        var template = _dataSourceContext.Templates.FirstOrDefault(d => d.Name.Equals(TemplateName));
        var dataSource =
            _dataSourceContext.DataSources.FirstOrDefault(d => template != null && d.Name.Equals(template.DataSource));
        if (dataSource == null)
        {
            // MessageBox.Show("未匹配到数据源", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        var matchCollection = Regex.Matches(dataSource.Script, @"(?<=@).*(?=[;| ])");
        

        var psList = _dataSourceContext.Parameters.ToList();
        foreach (var paramName in matchCollection)
        {
            var p = psList.FirstOrDefault(p => p.Name.Equals(paramName.ToString()));
            if (p == null)
            {
                continue;
            }
            _parameters.Add(p);
            var label = new TextBlock
            {
                Text = p.DisplayName,
                VerticalAlignment = VerticalAlignment.Center
            };
            parameterContainer.Children.Add(label);
            if (p.InputType==null)
            {
                continue;
            }
            var element = ParameterBuildFactory.Factory.Build(p);
            parameterContainer.Children.Add(element);
        }
    }
    
    private void LoadFile()
    {
        if (string.IsNullOrWhiteSpace(TemplateName))
        {
            System.Windows.Forms.MessageBox.Show(LangResource.Template_cant_null, LangResource.caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var path = string.Empty;
        var template = _dataSourceContext?.Templates.FirstOrDefault(d => d.Name.Equals(TemplateName));
        
        // TemplateName = AakToolWell.TemplateName;
        if (template != null)
        {
            _templateName = template.Name;
            path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                $"{template.Name}.xlsx");
            if (!File.Exists(path))
            {
                return;
            }
        }

        grid.CurrentWorksheet.Reset();
        // grid = new ReoGridControl();
        grid.Load(path, FileFormat._Auto, Encoding.Default);

        grid.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
    }


    [RelayCommand]
    private void OnQuery(object sender)=> QueryData();

    private void QueryData()
    {
        LoadFile();
        var dataSourceAndAddresses = new Dictionary<string, string>();
        grid.CurrentWorksheet.IterateCells(0, 0,  grid.CurrentWorksheet.ColumnCount,  grid.CurrentWorksheet.RowCount, true, (a, b, c) =>
        {
            if (!string.IsNullOrWhiteSpace(c.DisplayText))
            {
                if (Regex.IsMatch(c.DisplayText, "^\\{\\{.*\\}\\}$"))
                {
                    dataSourceAndAddresses.Add(c.DisplayText,c.Address);
                    Console.WriteLine($@"{a} {b} {c.DisplayText} {c.Data}");
                }
            }
            return true;
        });
           
        var dataSourceName = string.Empty;
        var result = new List<dynamic>();
        foreach (var d in dataSourceAndAddresses)
        {
           
            var temp = Regex.Match(d.Key, @"(?<=\{\{).*(?=\.)").Value;
            if (string.IsNullOrWhiteSpace(temp))
            {
                temp = TemplateName;
            }
            if (!temp!.Equals(dataSourceName))
            {
                dataSourceName = temp;
                result = GetDataSource(dataSourceName);
            }
            var fieldName = Regex.Match(d.Key, @"(?<=\.).*(?=\}\})").Value;
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                fieldName = Regex.Match(d.Key, @"(?<=\{\{).*(?=\}\})").Value;
            }
            var y = result.Count;
            if (y==0)
            {
                return;
            }
            grid.CurrentWorksheet.RowCount = y+1;
            var fieldValues = new object?[y, 1];
               
            for (var i = 0; i < y; i++)
            {
                var data = (IDictionary<string, object>)result[i];
                fieldValues[i,0 ] = data[fieldName].ToString();
            }

                
            grid.CurrentWorksheet[d.Value] = fieldValues;
        }
    }
    
    
    
    private List<dynamic> GetDataSource(string dataSourceName)
    {
        try
        {
            if (_dataSourceContext != null)
            {
                var ds = _dataSourceContext.DataSources.FirstOrDefault(c => c.Name.Equals(dataSourceName));

                if (ds != null)
                {
                    var conn = _dataSourceContext.Connections.FirstOrDefault(c => c.Name.Equals(ds.Connection));
                    if (conn != null)
                    {
                        var connectionString = GetConnectionString(conn.Host, conn.User, conn.Password,
                            conn.Database,
                            conn.Port.ToString());
                        using IDbConnection db = new NpgsqlConnection(connectionString);
// #if DEBUG
//                         ds.Script = "SELECT t.*\n              FROM public.scada_alarm t\n              LIMIT 5001";
// #endif
                        var matchCollection = Regex.Matches(ds.Script, @"(?<=@).*(?=[;| ])");
                        dynamic parameters = new ExpandoObject();
                        var dic = (IDictionary<string, object>)parameters;
                        foreach (Match o in matchCollection)
                        {
                            var parameter = _parameters.FirstOrDefault(p => p.Name.Equals(o.Value));
                            if (parameter==null)
                            {
                                continue;
                            }

                            
                            var c= VisualTreeHelperExtension.GetChildObject<ComboBox>(parameterContainer,o.Value);
                            if (c != null)
                            {
                                dic[o.Value] = GetParameterValue(o.Value);// c.SelectedValue;
                            }
                                
                            // // "低压配电室_Demo_502_40";
                        }
                        var result = db.Query(ds.Script,dic).ToList();
                        return result;
                    }
                }
            }

        }
        catch (RangeIntersectionException)
        {
            MessageBox.Show(LangResource.Msg_Range_Intersection_Exception);
        }
        catch(Exception e)
        {
            MessageBox.Show(e.Message);
        }
        return new List<dynamic>();
    }

    private string GetParameterValue( string name)
    {
        var parameter = _parameters.FirstOrDefault(p => p.Name.Equals(name));
        if (parameter==null)
        {
            return string.Empty;
        }

        switch (parameter.InputType)
        {
            
            case InputType.ComboBox:
                var comboBox = VisualTreeHelperExtension.GetChildObject<ComboBox>(parameterContainer, name);
                return comboBox?.SelectedValue?.ToString() ?? string.Empty;

            case InputType.TextBox:
                var textBox = VisualTreeHelperExtension.GetChildObject<TextBox>(parameterContainer, name);
                return textBox?.Text ?? string.Empty;

            case InputType.Date:
                var datePicker = VisualTreeHelperExtension.GetChildObject<DatePicker>(parameterContainer, name);
                return datePicker?.SelectedDate?.ToString("yyyy-MM-dd") ?? string.Empty;

            case InputType.CheckBox:
                var checkBox = VisualTreeHelperExtension.GetChildObject<CheckBox>(parameterContainer, name);
                return checkBox?.IsChecked.ToString() ?? string.Empty;

            // Add more cases as needed for other InputTypes

            default:
                return string.Empty;
        }
    }
    
    private string GetConnectionString(string servername, string uid, string pwd, string db, string port)
    {
        return $"host={servername};User ID={uid};password={pwd};database={db};port={port};pooling=false;";
    }
    
    [RelayCommand]
    private void OnReset(object sender)=> OnReset();

    private void OnReset()
    {
        
       
    }
}