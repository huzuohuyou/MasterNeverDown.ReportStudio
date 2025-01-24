// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.IO;
using System.Windows.Forms;
using MasterNeverDown.ReportStudio.Model.LangRes;
using unvell.ReoGrid;
using unvell.ReoGrid.IO;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars.Impl;

[ToolBar(nameof(NewFile))]
public class NewFile(ReoGridControl grid) :ToolbarBase(grid)
{
    public override void Execute(params object[] param)
    {
        if (!CloseDocument())
        {
            return;
        }

        grid.Reset();

        // this.Text = LangResource.Untitled + " - ReoGrid Editor " + this.ProductVersion;
        //
        // //showGridLinesToolStripMenuItem.Checked = workbook.HasSettings(ReoGridSettings.View_ShowGridLine);
        // this.CurrentFilePath = null;
        // this.currentTempFilePath = null;

#if DEBUG // for test case
        //showDebugFormToolStripButton.PerformClick();
        ForTest();
#endif
    }
    
    private void ForTest()
    {
    }
    
    private FileFormat GetFormatByExtension(string path)
    {
        if (String.IsNullOrEmpty(path))
        {
            return FileFormat._Auto;
        }

        var ext = Path.GetExtension(CurrentFilePath);

        if (ext != null && (ext.Equals(".rgf", StringComparison.CurrentCultureIgnoreCase)
                            || ext.Equals(".xml", StringComparison.CurrentCultureIgnoreCase)))
        {
            return FileFormat.ReoGridFormat;
        }

        if (ext != null && ext.Equals(".xlsx", StringComparison.CurrentCultureIgnoreCase))
        {
            return FileFormat.Excel2007;
        }
        if (ext != null && ext.Equals(".csv", StringComparison.CurrentCultureIgnoreCase))
        {
            return FileFormat.CSV;
        }
        return FileFormat._Auto;
    }

    private bool CloseDocument()
    {
        if (grid.IsWorkbookEmpty)
        {
            return true;
        }

        DialogResult dr = MessageBox.Show(LangResource.Msg_Save_Changes, LangResource.OpenFile_LoadFile_ReoGrid_Editor, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

        if (dr == DialogResult.No)
            return true;
        if (dr == DialogResult.Cancel)
            return false;

        FileFormat format = FileFormat._Auto;

        if (!String.IsNullOrEmpty(CurrentFilePath))
        {
            format = GetFormatByExtension(CurrentFilePath);
        }

        if (format == FileFormat._Auto || String.IsNullOrEmpty(CurrentFilePath))
        {
            return SaveAsDocument();
        }

        SaveDocument();

        return true;
    }

    
}