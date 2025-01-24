using CommunityToolkit.Mvvm.Input;
using MasterNeverDown.ReportStudio.Model.LangRes;

namespace AakStudio.Shell.UI.Showcase.ViewModels;

public sealed partial class TemplateViewModel : AakToolWell,IRefreshList
{
    private DataSourceContext? _dataSourceContext =
        App.ServiceProvider.GetService(typeof(DataSourceContext)) as DataSourceContext;

     
    protected override void DeleteEntityByName()
    {
        if (_dataSourceContext == null) return;
        var entity = _dataSourceContext.Templates.FirstOrDefault(s => s.Name.Equals(Name));
        if (entity != null) _dataSourceContext.Templates.Remove(entity);
        _dataSourceContext.SaveChanges();
    }

    public TemplateViewModel(WorkSpaceViewModel workSpaceViewModel)
    {
        this.workSpaceViewModel = workSpaceViewModel;

        Title = "模板";
        collections = new ObservableCollection<AakCollectionViewModel>
        {
            new BasicCollectionViewModel(this)
        };
        View = new TemplateView { DataContext = this };
        Load();
    }

    [RelayCommand]
    private void OnActive(object sender)
    {
        var dialog = new InputFileNameDialog();
        var result = dialog.ShowDialog();
        if (result == true)
        {
            var templateName = dialog.FileName;
            MessageBox.Show(LangResource.Your_request_will_be_processed);
            WorkSpaceViewModel.Default.AddNewLayoutDocument(templateName, templateName);
        }
        else
        {
            MessageBox.Show(LangResource.try_again_later);
        }
    }


    [RelayCommand]
    private void OnCreate(object sender)
    {
        var dialog = new InputFileNameDialog
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };
        var result = dialog.ShowDialog();
        if (result == true)
        {
            var templateName = dialog.FileName;
            var path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                $"{templateName}.xlsx");
            if (File.Exists(path))
            {
                MessageBox.Show("已存在模板请重新命名");
                return;
            }

            var g = WorkSpaceViewModel.Default.AddNewLayoutDocument(templateName, templateName);

            if (_dataSourceContext == null)
                return;
            _dataSourceContext.Templates.Add(new Template
            {
                Name = templateName
            });
            _dataSourceContext.SaveChanges();

            SaveFile(g, path);
            AddOrUpdate(templateName);
        }
        else
        {
            MessageBox.Show(LangResource.try_again_later);
        }
    }

    private static void SaveFile(ReoGridControl grid, string? path)
    {
#if EX_SCRIPT
			if (scriptEditor != null && scriptEditor.Visible)
			{
				this.grid.Script = scriptEditor.Script;
			}
#endif // EX_SCRIPT

        //, "ReoGridEditor " + this.ProductVersion.ToString())
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
            grid.Save(path, fm);


#if DEBUG
            // RGUtility.OpenFileOrLink(path);
#endif
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(null, LangResource.Save_Error + ex.Message,
                LangResource.Save_Workbook,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    public void Load()
    {
        _dataSourceContext = App.ServiceProvider.GetService(typeof(DataSourceContext)) as DataSourceContext;
        // 假设 _dataSourceContext 是数据源上下文
        if (_dataSourceContext == null) return;
        var connectionsFromDb = _dataSourceContext.Templates.ToList();
        var b = new BasicCollectionViewModel(this);

        var item = new ObservableCollection<AakDocumentWell>();

        var temp = connectionsFromDb.Select(c =>
            new AakDocumentWellViewModel(c.Name, b));
        foreach (var vm in temp)
        {
            item.Add(vm);
        }

        Collections = [b];
        b.Items = item;
    }

    public void AddOrUpdate(string name)
    {
        Load();
        ActiveDocument(name);
    }
}