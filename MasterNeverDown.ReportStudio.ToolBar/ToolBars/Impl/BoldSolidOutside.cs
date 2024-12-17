using System.Drawing;
using System.Windows.Forms;
using MasterNeverDown.ReportStudio.ToolBar.LangRes;
using unvell.ReoGrid;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars.Impl;

public class BoldSolidOutside(ReoGridControl grid) :ToolbarBase(grid)
{
    public override void Execute(params object[] param)
    {
        try
        {
            var v = param[0].ToString();
            Enum.TryParse<BorderLineStyle>(param[0].ToString(), out var value);
            grid.CurrentWorksheet.SetRangeBorders(
                grid.CurrentWorksheet.Ranges[CurrentSelectionRange],
                BorderPositions.Outside, new RangeBorderStyle
                {
                    Style = value, // as  BorderLineStyle,//BorderLineStyle
                    Color = unvell.ReoGrid.Graphics.SolidColor.Black,
                });
        }
        catch (RangeIntersectionException)
        {
            MessageBox.Show(LangResource.Msg_Range_Intersection_Exception);
        }
        catch
        {
            MessageBox.Show(LangResource.Msg_Operation_Aborted);
        }
    }
}