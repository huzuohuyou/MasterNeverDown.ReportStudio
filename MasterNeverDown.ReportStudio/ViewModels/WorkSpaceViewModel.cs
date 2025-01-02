using AvalonDock;
using AvalonDock.Layout;
using CommunityToolkit.Mvvm.Input;

namespace AakStudio.Shell.UI.Showcase.ViewModels;

public partial class WorkSpaceViewModel : ViewModelBase
{
    public static WorkSpaceViewModel Default { get; } = new();
    private CultureInfo _selectedLanguage;


    public ObservableCollection<CultureInfo> Languages { get; }

    public CultureInfo SelectedLanguage
    {
        get
        {
            string selectedLanguage = ConfigHelper.GetSelectedLanguage();
            _selectedLanguage = new CultureInfo(selectedLanguage);
            return _selectedLanguage ;
        }
        set
        {
            if (Equals(_selectedLanguage, value)) return;
            _selectedLanguage = value;
            
            ConfigHelper.UpdateAppConfig("SelectedLanguage", _selectedLanguage.Name);
            OnPropertyChanged(ref _selectedLanguage, value, nameof(SelectedLanguage));
            RestartApplication();
        }
    }


    private void RestartApplication()
    {
        System.Diagnostics.Process.Start(System.Diagnostics.Process.GetCurrentProcess().MainModule!.FileName!);
        Application.Current.Shutdown();
    }

    public ObservableCollection<AakToolWell> Anchorables
    {
        get => anchorables;
        set => OnPropertyChanged(ref anchorables, value, nameof(Anchorables));
    }

    public ObservableCollection<AakDocumentWell?> DocumentViews
    {
        get => documentViews;
        set => OnPropertyChanged(ref documentViews, value, nameof(DocumentViews));
    }

    public AakTheme CurrentTheme
    {
        get => currentTheme;
        set
        {
            AakXamlUIResource.Instance.Theme = value;
            OnPropertyChanged(ref currentTheme, value, nameof(CurrentTheme));
        }
    }

    public AakViewElement? ActiveDocument
    {
        get => activeDocument;
        set => OnPropertyChanged(ref activeDocument, value, nameof(ActiveDocument));
    }

    public ICommand ThemeSwitchCommand => themeSwitchCommand ??= new Commands.RelayCommand<AakTheme>(OnThemeSwitch);

    public WorkSpaceViewModel()
    {
        StyleSelector = new StyleSelectorViewModel(this);
        Connection = new ConnectionViewModel(this);
        DataSource = new DataSourceViewModel(this);
        Template = new TemplateViewModel(this);
        Parameter = new ParameterViewModel(this);
        currentTheme = AakXamlUIResource.Instance.Theme;

        anchorables = new ObservableCollection<AakToolWell>
            { StyleSelector, DataSource, Template, Connection, Parameter };
        documentViews = new ObservableCollection<AakDocumentWell?>();
        Languages = new ObservableCollection<CultureInfo>
        {
            new("en-US"),
            new("zh-hans")
        };
        LangResource.Culture = SelectedLanguage;
    }

   

    public StyleSelectorViewModel StyleSelector { get; }
    public ParameterViewModel Parameter { get; }
    public ConnectionViewModel Connection { get; }
    public DataSourceViewModel DataSource { get; }
    public TemplateViewModel Template { get; }

    [ObservableProperty] private string templateName = null!;
    [ObservableProperty] private ObservableCollection<string> _dateSources = null!;

    private ObservableCollection<AakToolWell> anchorables;
    private ObservableCollection<AakDocumentWell?> documentViews;
    private AakTheme currentTheme;
    private AakViewElement? activeDocument;
    private ICommand? themeSwitchCommand;
    private ReoGridControl grid = null!;
    private DockingManager _reportDockingManager = null!;

    private void OnThemeSwitch(AakTheme? newTheme)
    {
        if (newTheme is not null)
        {
            CurrentTheme = newTheme;
        }
    }

    public void AddOrActiveDocument(AakDocumentWell view)
    {
        var item = DocumentViews.FirstOrDefault(x => x is { Title: not null } && x.Title.Equals(view.Title));
        if (item == null)
        {
            item = view;
            DocumentViews.Add(item);
        }

        ActiveDocument = item;
    }

    public void AddOrActiveDocument(string viewName)
    {
        var view = DocumentViews.FirstOrDefault(v => v is { Title: not null } && v.Title.Equals(viewName));
        if (view == null)
        {
            return;
        }

        var item = DocumentViews.FirstOrDefault(x => x == view);
        if (item == null)
        {
            item = view;
            DocumentViews.Add(item);
        }

        ActiveDocument = item;
    }

    public void CloseDocument(AakDocumentWell view)
    {
        if (DocumentViews.Contains(view))
        {
            DocumentViews.Remove(view);
            ActiveDocument = DocumentViews.FirstOrDefault();
        }
    }

    private void LoadFile(string? path, ReoGridControl gridControl)
    {
        if (!File.Exists(path))
        {
            return;
        }

        gridControl.Load(path, FileFormat._Auto, Encoding.Default);
    }

    [RelayCommand]
    private void OnLoadedDockingManager(object sender)
    {
        _reportDockingManager = (DockingManager)sender;
    }

    public ReoGridControl AddNewLayoutDocument(string title, string contentId)
    {
        ActiveDocument:
        var document = _reportDockingManager.Layout.Descendents().OfType<LayoutDocument>()
            .FirstOrDefault(d => d.ContentId == contentId);
        if (document != null)
        {
            _reportDockingManager.ActiveContent = document;
            return grid;
        }

        var documentPane = _reportDockingManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
        if (documentPane != null)
        {
            var content = new ReportDesigner();
            var vm = new TemplateEditorViewModel
            {
                TemplateName = contentId
            };
            using var dataSourceContext = new DataSourceContext();
            var currentTemplate = dataSourceContext.Templates.FirstOrDefault(d => d.Name.Equals(TemplateName));
            vm.SelectedDataSource = currentTemplate?.DataSource!;

            content.DataContext = vm;
            var newDocument = new LayoutDocument
            {
                Title = title,
                ContentId = contentId,
                Content = content
            };
            grid = content.Grid;
            documentPane.Children.Add(newDocument);

            var pathJoin = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                $"{contentId}.xlsx");
            LoadFile(pathJoin, content.Grid);

            grid.CurrentWorksheet.ColumnCount = currentTemplate?.ColumnCount ?? 100;
            grid.CurrentWorksheet.RowCount = currentTemplate?.RowCount ?? 200;
        }

        goto ActiveDocument;
    }
}