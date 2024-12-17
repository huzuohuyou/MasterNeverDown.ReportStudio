using CommunityToolkit.Mvvm.Input;

namespace AakStudio.Shell.UI.Showcase.Shell;

public abstract partial class AakToolWell : AakViewElement
{
    public bool CanHide
    {
        get => canHide;
        set => SetProperty(ref canHide, value);
    }

    public bool IsVisible
    {
        get => isVisible;
        set => SetProperty(ref isVisible, value);
    }

    private bool canHide = true;
    private bool isVisible = true;
        
    public  WorkSpaceViewModel workSpaceViewModel = null!;
    public ObservableCollection<AakCollectionViewModel> collections;

    public static string pathJoin;
    // public static string TempKlateName;
        
    public void ActiveDocument(AakDocumentWell view)
    {
        if (view.View!=null)
        {
                
            workSpaceViewModel.AddOrActiveDocument(view);
        }
        else
        {
            pathJoin = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                $"{view.Title}.xlsx");
            if (!File.Exists(pathJoin)) return;
            if (view.Title == null) return;
            workSpaceViewModel.TemplateName = view.Title;
            workSpaceViewModel.AddNewLayoutDocument(view.Title, view.Title);
        }
           
    }
        
    public void ActiveDocument(string view)
    {
        workSpaceViewModel.AddOrActiveDocument(view);
    }

    public void CloseTab(AakDocumentWell view)
    {
        workSpaceViewModel.CloseDocument(view);
    }
        
    public ObservableCollection<AakCollectionViewModel> Collections
    {
        get => collections;
        set => SetProperty(ref collections, value);
    }
        
    private object? selectedItem;

    public object? SelectedItem
    {
        get => selectedItem;
        set => SetProperty(ref selectedItem, value);
    }

    protected string? Name;

    [RelayCommand]
    private void OnSelectItemChanged(object? s)
    {
        SelectedItem = s;
        if (selectedItem is AakDocumentWellViewModel vm)
        {
            Name = vm.Title;
        }
    }

    [RelayCommand]
    private void OnDelete()
    {
        if (SelectedItem is AakDocumentWellViewModel vm)
        {
            var aakDocumentWells = Collections.FirstOrDefault()?.Items;
            if (aakDocumentWells != null)
            {
                var item = aakDocumentWells.FirstOrDefault(c => c.Title != null && c.Title.Equals(vm.Title));
                if (item != null) aakDocumentWells.Remove(item);
            }

            SelectedItem = null;
            DeleteEntityByName();
        }
    }

    protected abstract void DeleteEntityByName();
}