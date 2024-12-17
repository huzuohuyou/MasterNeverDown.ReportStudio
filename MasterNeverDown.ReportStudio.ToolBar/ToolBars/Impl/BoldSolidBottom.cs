using System.Windows.Forms;
using MasterNeverDown.ReportStudio.ToolBar.LangRes;
using unvell.ReoGrid;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars.Impl;

public class BoldSolidBottom(ReoGridControl grid) :ToolbarBase(grid)
{
    public override void Execute(params object[] param)
    {
        try
        {
            grid.CurrentWorksheet.SetRangeBorders(
                grid.CurrentWorksheet.Ranges[CurrentSelectionRange], 
                BorderPositions.Bottom,
                RangeBorderStyle.BlackSolid);
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