

namespace AakStudio.Shell.UI.Showcase.Editors;

public static class GridExtentsion
{
    public static CellPosition PointToCell(this ReoGridControl grid, Point pos)
    {
   
        return new CellPosition(grid.CurrentWorksheet.SelectionRange.Row, grid.CurrentWorksheet.SelectionRange.Col);
    }
}
