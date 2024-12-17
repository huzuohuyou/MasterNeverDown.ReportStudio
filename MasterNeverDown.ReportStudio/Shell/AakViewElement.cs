using CommunityToolkit.Mvvm.ComponentModel;

namespace AakStudio.Shell.UI.Showcase.Shell;

public abstract class AakViewElement : ObservableObject
{
    public UIElement? View
    {
        get => view;
        set => SetProperty(ref view, value);
    }
        
    public string? Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public bool IsActive
    {
        get => isActive;
        set => SetProperty(ref isActive, value);
    }

    public bool IsSelected
    {
        get => isSelected;
        set => SetProperty(ref isSelected, value);
    }

    private UIElement? view;
    private string? title;
    private bool isActive;
    private bool isSelected;
}