// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows.Forms;
using MasterNeverDown.ReportStudio.Model.LangRes;
using unvell.ReoGrid;
using unvell.ReoGrid.IO;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars.Impl;

[ToolBar(nameof(Impl.SaveFile))]
public class SaveFile(ReoGridControl grid) :ToolbarBase(grid)
{
    public override void Execute(params object[] param)
    {
        SaveFile2(AppDomain.CurrentDomain.BaseDirectory);
    }
    
    private void SaveFile2(string? path)
    {
        FileFormat fm = FileFormat._Auto;

        if (path != null && path.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase))
        {
            fm = FileFormat.Excel2007;
        }
        else if (path != null && path.EndsWith(".rgf", StringComparison.CurrentCultureIgnoreCase))
        {
            fm = FileFormat.ReoGridFormat;
        }
        else if (path != null && path.EndsWith(".csv", StringComparison.CurrentCultureIgnoreCase))
        {
            fm = FileFormat.CSV;
        }

        try
        {
            grid.Save(path, fm);
        }
        catch (Exception ex)
        {
            MessageBox.Show(null, LangResource.Save_Error + ex.Message, LangResource.Save_Workbook, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

}