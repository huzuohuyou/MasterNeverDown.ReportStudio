using HandyControl.Controls;
using LangResource = MasterNeverDown.ReportStudio.ToolBar.LangRes.LangResource;

using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommandAttribute;

namespace AakStudio.Shell.UI.Showcase.ViewModels;

public partial class TemplateEditorViewModel : AakToolWell
{
    private readonly DataSourceContext? _dataSourceContext =
        App.ServiceProvider.GetService(typeof(DataSourceContext)) as DataSourceContext;

    [ObservableProperty] private string templateName = null!;
    private ReoGridControl? grid;
    [ObservableProperty] private string _selectedDataSource = null!;
    private ComboBox _comboBox = null!;
    [ObservableProperty] private int _rowCount;
    [ObservableProperty] private int _columnCount;
    public ObservableCollection<string> DateSources { get; set; } = new();

    public TemplateEditorViewModel()
    {
        LoadConnections();
    }

    private void LoadConnections()
    {
        if (_dataSourceContext == null) return;
        var connectionsFromDb = _dataSourceContext.DataSources.Select(s => s.Name).ToList();
        DateSources = new ObservableCollection<string>(connectionsFromDb);
    }


    [RelayCommand]
    private void OnLoadedWindow(object sender)
    {
        LoadDateSources();
        grid = (ReoGridControl)sender;
    }

    private void LoadDateSources()
    {
        if (_dataSourceContext == null) return;
        var connectionsFromDb = _dataSourceContext.DataSources.Select(s => s.Name).ToList();
        DateSources = new ObservableCollection<string>(connectionsFromDb);
    }


    [RelayCommand]
    private void OnNew(object sender) => NewFile(pathJoin);

    private void NewFile(string? path)
    {
        pathJoin = string.Empty;
        var fm = FileFormat._Auto;

        if (path != null && path.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase))
        {
            fm = FileFormat.Excel2007;
        }
        else if (path != null && path.EndsWith(".rgf", StringComparison.CurrentCultureIgnoreCase))
        {
            fm = FileFormat.ReoGridFormat;
        }
        else if (path != null && path.EndsWith(".csv", StringComparison.CurrentCultureIgnoreCase))
        {
            fm = FileFormat.CSV;
        }

        try
        {
            grid?.Save(path, fm);
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(null, LangResource.Save_Error + ex.Message, LangResource.Save_Workbook,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }


    [RelayCommand]
    private void OnLoadedDataSourceComboBox(object sender)
    {
        _comboBox = (ComboBox)sender;
    }

    [RelayCommand]
    private void OnLoadedTemplateName(object sender)
    {
    }

    [RelayCommand]
    private void OnSave(object parameter)
    {
        if (parameter is not object[] values)
        {
            System.Windows.Forms.MessageBox.Show(LangResource.Unexpected_parameter);
            return;
        }

        grid = values[0] as ReoGridControl;
        if (grid is null) return;

        if (string.IsNullOrWhiteSpace(TemplateName))
        {
            var dialog = new SaveTemplate();
            var result = dialog.ShowDialog();
            System.Windows.Forms.MessageBox.Show(result == true
                ? LangResource.Your_request_will_be_processed
                : LangResource.try_again_later);
        }

        if (_dataSourceContext == null) return;
        var existingDataSource = _dataSourceContext.Templates.FirstOrDefault(ds => ds.Name == TemplateName);

        if (existingDataSource != null)
        {
            existingDataSource.Name = TemplateName;
            existingDataSource.DataSource = SelectedDataSource;
            existingDataSource.ColumnCount = ColumnCount;
            existingDataSource.RowCount = RowCount;
        }
        else
        {
            _dataSourceContext.Templates.Add(new Template
            {
                Name = TemplateName,
                DataSource = _comboBox.SelectedValue.ToString(),
                ColumnCount = ColumnCount,
                RowCount = ColumnCount,
            });
        }

        SaveFile(pathJoin??$"{AppDomain.CurrentDomain.BaseDirectory}{TemplateName}.xlsx");
        _dataSourceContext.SaveChanges();
    }

    private void SaveFile(string? path)
    {
        var fm = FileFormat._Auto;

        if (path != null && path.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase))
        {
            fm = FileFormat.Excel2007;
        }
        else if (path != null && path.EndsWith(".rgf", StringComparison.CurrentCultureIgnoreCase))
        {
            fm = FileFormat.ReoGridFormat;
        }
        else if (path != null && path.EndsWith(".csv", StringComparison.CurrentCultureIgnoreCase))
        {
            fm = FileFormat.CSV;
        }

        try
        {
            grid?.Save(path, fm);
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(null, LangResource.Save_Error + ex.Message, LangResource.Save_Workbook,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private static void Do(ReoGridControl doc, string command, params object[] p)
    {
        var ass = Assembly.LoadFile($"{AppDomain.CurrentDomain.BaseDirectory}MasterNeverDown.ReportStudio.ToolBar.dll");
        var fullName = $"MasterNeverDown.ReportStudio.ToolBar.ToolBars.Impl.{command}";
        var t = ass.GetType(fullName);

        if (t == null)
        {
            MessageBox.Show(LangResource.UnExist_Execute_Method);
            return;
        }

        var o = Activator.CreateInstance(t, doc);
        var mi = t.GetMethod("Execute");
        if (mi != null)
        {
            mi.Invoke(o, [p]);
        }
        else
        {
            System.Windows.Forms.MessageBox.Show(LangResource.UnExist_Execute_Method);
        }
    }

    [RelayCommand]
    private void OnPreview(object sender)
    {
        if (!string.IsNullOrWhiteSpace(TemplateName))
        {
            var dialog = new PreviewTemplateView(TemplateName);
            dialog.Show();
        }
        else
        {
            System.Windows.Forms.MessageBox.Show(LangResource.OnPreview);
        }
    }

    [RelayCommand]
    private void OnToolBarExecute(object parameter)
    {
        if (parameter is not object[] values)
        {
            System.Windows.Forms.MessageBox.Show(LangResource.Unexpected_parameter);
            return;
        }

        var doc = values[0] as ReoGridControl;
        if (doc is null) return;
        if (values[1] is Button button)
        {
            if (values is [_, _, string v])
            {
                Do(doc, button.Name, [v]);
            }
            else if (values.Length == 2)
            {
                Do(doc, button.Name);
            }
        }

        if (values[1] is ToggleButton toggleButton)
            Do(doc, toggleButton.Name);
        if (values[1] is ColorPicker colorPicker)
            Do(doc, colorPicker.Tag.ToString()!, [colorPicker.SelectedBrush, values[2]]);
    }

    protected override void DeleteEntityByName()
    {
        throw new NotImplementedException();
    }
}