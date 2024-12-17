// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using MasterNeverDown.ReportStudio.ToolBar.LangRes;
using unvell.ReoGrid;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars.Impl;

public class FillColor(ReoGridControl grid) :ToolbarBase(grid)
{
    public override void Execute(params object[] param)
    {
        try
        {
            if (param[0] is SolidColorBrush color)
                grid.CurrentWorksheet.Ranges[CurrentSelectionRange].Style.BackColor = color.Color;
            if (param[1] is Popup popup)
                popup.IsOpen = false;
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