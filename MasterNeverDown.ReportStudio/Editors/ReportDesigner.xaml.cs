

namespace AakStudio.Shell.UI.Showcase.Editors;

public partial class ReportDesigner : System.Windows.Controls.UserControl
{
    public ReportDesigner()
    {
        InitializeComponent();
        
    }

    private void ReoGridControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var grid = sender as ReoGridControl;
        grid.CurrentWorksheet.SelectionRange = new RangePosition(grid.PointToCell(e.GetPosition(grid)));
        
        // ReoGridControl.CurrentWorksheet.BeforeCellKeyDown += (s, e) =>
        // {
        //     if (e.KeyCode == (KeyCode)Keys.Enter)
        //     {
        //         e.IsCancelled = false;
        //         // always move to first cell of next row
        //         ReoGridControl.CurrentWorksheet.SelectionRange =
        //             new RangePosition(ReoGridControl.CurrentWorksheet.SelectionRange.Row + 1, 0, 1, 1);
        //     }
        // };

        grid.CurrentWorksheet.StartEdit(grid.CurrentWorksheet.SelectionRange.StartPos);
    }

    private void ReportDesigner_OnLoaded(object sender, RoutedEventArgs e)
    {
        ReoGridControl.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
        ReoGridControl.CurrentWorksheet.SelectionMode = WorksheetSelectionMode.Range;
        ReoGridControl.CurrentWorksheet.BeforeCellEdit += (sender, eventArgs) => {
            // Prevent cell editing by setting IsCancelled to true
            eventArgs.IsCancelled = false;
        };
        ReoGridControl.CurrentWorksheet.CellMouseDown += sheet_CellMouseDown;
    }

    private void sheet_CellMouseDown(object? sender, CellMouseEventArgs e)
    {

        // safe: cell instance created from position if not existed
        var sheet = ReoGridControl.CurrentWorksheet;
        var cell = sheet.CreateAndGetCell(e.CellPosition);
    }
}