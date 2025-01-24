// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows.Forms;
using MasterNeverDown.ReportStudio.Model.LangRes;
using unvell.ReoGrid;
using unvell.ReoGrid.Actions;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars.Impl;

[ToolBar("MergeSelectionRange")]
public class MergeSelectionRange(ReoGridControl grid) :ToolbarBase(grid)
{
    public override void Execute(params object[] param)
    {
        try
        {
            grid.DoAction(new MergeRangeAction(CurrentWorksheet.SelectionRange));
        }
        catch (RangeTooSmallException) { }
        catch (RangeIntersectionException)
        {
            MessageBox.Show(LangResource.Msg_Range_Intersection_Exception,
                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}