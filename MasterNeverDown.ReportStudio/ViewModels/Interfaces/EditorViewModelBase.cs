namespace AakStudio.Shell.UI.Showcase.ViewModels.Interfaces;

public abstract class EditorViewModelBase : AakToolWell
{
    protected abstract override void DeleteEntityByName();

    protected abstract void SaveEntity();

    protected abstract void LoadEntity();
}