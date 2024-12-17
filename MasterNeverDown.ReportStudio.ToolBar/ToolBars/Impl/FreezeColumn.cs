// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows;
using MasterNeverDown.ReportStudio.ToolBar.LangRes;
using unvell.ReoGrid;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars.Impl;

public class FreezeColumn(ReoGridControl grid) : ToolbarBase(grid)
{
    public override void Execute(params object[] param)
    {
        try
        {
            var sheet = grid.CurrentWorksheet;

            sheet.FreezeToCell(0,CurrentSelectionRange.EndPos.Col+1);
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