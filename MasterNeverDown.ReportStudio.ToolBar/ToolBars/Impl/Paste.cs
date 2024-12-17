// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows;
using unvell.ReoGrid;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars.Impl;

public class Paste(ReoGridControl grid) : ToolbarBase(grid)
{
    public override void Execute(params object[] param)
    {
        try
        {
            CurrentWorksheet.Paste();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}