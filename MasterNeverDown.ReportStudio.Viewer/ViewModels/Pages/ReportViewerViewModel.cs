using TextBlock = Wpf.Ui.Controls.TextBlock;

namespace MasterNeverDown.ReportStudio.Viewer.ViewModels.Pages;

public partial class ReportViewerViewModel(DataSourceContext dataSourceContext) : ObservableObject, INavigationAware
{
    private readonly bool _isInitialized = false;
    private static string TemplateName;
    private ReoGridControl _grid = null!;
    private StackPanel _parameterContainer = null!;

    public void OnNavigatedTo()
    {
        if (_parameterContainer!=null)
        { _grid.Reset();
            InitializeViewModel();
        }
    }


    [RelayCommand]
    private void OnNavigationItemClick(string itemName)
    {
        TemplateName = itemName;
      
    }

    [RelayCommand]
    private void OnLoadedWindow(object sender)
    {
        var parameters = (object[])sender;

        _grid = (ReoGridControl)parameters[0];
        _grid.Reset();
        _parameterContainer = (StackPanel)parameters[1];
        _grid.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
        InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        LoadParameters();
        LoadFile();
    }

    private readonly List<Parameter> _parameters = [];

    private void LoadParameters()
    {
        if (string.IsNullOrWhiteSpace(TemplateName))
        {
            System.Windows.Forms.MessageBox.Show(LangResource.Template_cant_null, LangResource.caption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _parameters.Clear();
        var template = dataSourceContext.Templates.FirstOrDefault(d => d.Name.Equals(TemplateName));
        var dataSource =
            dataSourceContext.DataSources.FirstOrDefault(d => template != null && d.Name.Equals(template.DataSource));
        if (dataSource == null)
        {
            // MessageBox.Show("未匹配到数据源", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var matchCollection = Regex.Matches(dataSource.Script, @"(?<=@).*(?=[;| ])");


        var psList = dataSourceContext.Parameters.ToList();
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
            _parameterContainer.Children.Add(label);
            if (p.InputType == null)
            {
                continue;
            }

            var element = ParameterBuildFactory.Factory.Build(p);
            _parameterContainer.Children.Add(element);
        }
    }

    private void LoadFile()
    {
        if (string.IsNullOrWhiteSpace(TemplateName))
        {
            System.Windows.Forms.MessageBox.Show(LangResource.Template_cant_null, LangResource.caption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var path = string.Empty;
        var template = dataSourceContext.Templates.FirstOrDefault(d => d.Name.Equals(TemplateName));

        // TemplateName = AakToolWell.TemplateName;
        if (template != null)
        {
            TemplateName = template.Name;
            path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                $"{template.Name}.xlsx");
            if (!File.Exists(path))
            {
                return;
            }
        }

        _grid.CurrentWorksheet.Reset();
        // grid = new ReoGridControl();
        _grid.Load(path, FileFormat._Auto, Encoding.Default);

        _grid.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
    }

    public void OnNavigatedFrom()
    {
    }
}