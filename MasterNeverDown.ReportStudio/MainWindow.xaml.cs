using System.Runtime.CompilerServices;
using AakStudio.Shell.UI.Controls;
using AvalonDock.Layout;

namespace AakStudio.Shell.UI.Showcase;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public sealed partial class MainWindow : CustomChromeWindow
{
   
    public MainWindow()
    {
        InitializeComponent();
        
    }

    private bool isCleanValue;

    private void DockingManager_ActiveContentChanged(object sender, System.EventArgs e)
    {
        if (!isCleanValue && IsNotToolWell(ReportDockingManager.ActiveContent))
        {
            ClearValue(ActiveGlowBrushProperty);

            mainStatusBar.ClearValue(BackgroundProperty);
            mainStatusBar.ClearValue(ForegroundProperty);

            isCleanValue = true;
        }
        // On Theme Changed
        else if (IsActive && IsNotToolWell(ReportDockingManager.ActiveContent))
        {
            ActiveContentOfDockingManager();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsNotToolWell(object value)
    {
        return value is AakCollectionViewModel || value is AakDocumentWellViewModel;
    }

    private void MetroWindow_Activated(object sender, System.EventArgs e)
    {
        ActiveContentOfDockingManager();
    }

    private void ActiveContentOfDockingManager()
    {
        // if the window is activated, then active the last actived item in docking manager
        var hasFloatingWindow = false;

        var items = ReportDockingManager.Layout.Descendents().OfType<LayoutContent>().ToList();
        for (var i = 0; i < items.Count; i++)
        {
            var item = items[i];
            if (item.IsFloating)
            {
                if (!hasFloatingWindow)
                    hasFloatingWindow = true;
                items.RemoveAt(i--);
            }
        }

        if (hasFloatingWindow && items.Count > 0)
        {
            var index = 0;

            var tmpTimeStamp = items[0].LastActivationTimeStamp;
            for (var j = 1; j < items.Count; j++)
            {
                var item2 = items[j];
                if (item2.LastActivationTimeStamp > tmpTimeStamp)
                {
                    tmpTimeStamp = item2.LastActivationTimeStamp;
                    index = j;
                }
            }

            items[index].IsActive = true;
        }
    }
}