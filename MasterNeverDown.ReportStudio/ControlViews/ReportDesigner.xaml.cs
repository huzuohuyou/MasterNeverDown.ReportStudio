

// ReSharper disable RedundantExtendsListEntry

namespace AakStudio.Shell.UI.Showcase.ControlViews;

public partial class ReportDesigner : UserControl
{
    public ReportDesigner()
    {
        InitializeComponent();
    }

    private void Grid_OnCellMouseDown(object sender, unvell.ReoGrid.Events.CellMouseEventArgs e)
    {
        var c = sender as Worksheet;
        if (c?.Cells[e.CellPosition] is not { } cell) return;
        Bold.IsChecked = cell.Style.Bold;
        Italic.IsChecked = cell.Style.Italic;
        Underline.IsChecked = cell.Style.Underline;
        FontSize.Text = cell.Style.FontSize.ToString(CultureInfo.InvariantCulture);
        FontName.Text = cell.Style.FontName;
        TextColor.Background = ConvertToBrush(cell.Style.TextColor);
        BackColor.Background = ConvertToBrush(cell.Style.BackColor);
        //HAlign
        HorizontalAlignCenter.IsChecked = cell.Style.HAlign == ReoGridHorAlign.Center;
        HorizontalAlignRight.IsChecked = cell.Style.HAlign == ReoGridHorAlign.Right;
        HorizontalAlignLeft.IsChecked = cell.Style.HAlign == ReoGridHorAlign.Left;
        //VAlign
        VerticalAlignMiddle.IsChecked = cell.Style.VAlign == ReoGridVerAlign.Middle;
        VerticalAlignBottom.IsChecked = cell.Style.VAlign == ReoGridVerAlign.Bottom;
        VerticalAlignTop.IsChecked = cell.Style.VAlign == ReoGridVerAlign.Top;
    }

    private static Brush ConvertToBrush(SolidColor solidColor)
    {
        return new SolidColorBrush(Color.FromArgb(255, solidColor.R, solidColor.G, solidColor.B));
    }

    private void Select_Color_Click(object sender, RoutedEventArgs e)
    {
        PopupElement.PlacementTarget = sender as UIElement;
        PopupElement.IsOpen = !PopupElement.IsOpen;
        if (sender is Button b)
        {
            SelectColor.Tag = b.Name;
        }
    }

    private void ColorPicker_OnCanceled(object? sender, EventArgs e)
    {
        PopupElement.IsOpen = false;
    }


    private void Grid_OnLoaded(object sender, RoutedEventArgs e)
    {
        Grid.CurrentWorksheet.CellMouseDown += Grid_OnCellMouseDown!;
        Rows.NumericValue=Grid.CurrentWorksheet.RowCount;
        Columns.NumericValue=Grid.CurrentWorksheet.ColumnCount;
    }

    private void OnIncrementValue(object sender, RoutedEventArgs e)
    {
        Grid.CurrentWorksheet.RowCount++;
    }

    private void OnDecrementValue(object sender, RoutedEventArgs e)
    {
        Grid.CurrentWorksheet.RowCount--;
    }

    private void Columns_OnIncrementValue(object sender, RoutedEventArgs e)
    {
        Grid.CurrentWorksheet.ColumnCount++;
    }

    private void Columns_OnDecrementValue(object sender, RoutedEventArgs e)
    {
        Grid.CurrentWorksheet.ColumnCount--;
    }
}