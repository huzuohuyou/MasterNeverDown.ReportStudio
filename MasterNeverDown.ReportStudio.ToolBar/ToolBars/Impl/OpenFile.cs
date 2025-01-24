// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.IO;
using System.Text;
using System.Windows.Forms;
using MasterNeverDown.ReportStudio.Model.LangRes;
using unvell.ReoGrid;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars.Impl;

[ToolBar(nameof(OpenFile))]
public class OpenFile(ReoGridControl grid) :ToolbarBase(grid)
{
    public override void Execute(params object[] param)
    {
        using var ofd = new OpenFileDialog();
        ofd.Filter= LangResource.Filter_Load_File;
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            LoadFile(ofd.FileName);
            SetCurrentDocumentFile(ofd.FileName);
        }
    }

    private void LoadFile(string? path)
    {
        LoadFile(path, Encoding.Default);
    }

    private void LoadFile(string? path, Encoding encoding)
    {
        CurrentFilePath = null;

        bool success;

        grid.CurrentWorksheet.Reset();

        try
        {
            grid.Load(path, unvell.ReoGrid.IO.FileFormat._Auto, encoding);
            success = true;
        }
        catch (FileNotFoundException ex)
        {
            success = false;
            MessageBox.Show(LangResource.Msg_File_Not_Found+ ex.FileName,LangResource.OpenFile_LoadFile_ReoGrid_Editor, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        catch (Exception ex)
        {
            success = false;
            MessageBox.Show(LangResource.Msg_Load_File_Failed+ ex.Message, LangResource.OpenFile_LoadFile_ReoGrid_Editor, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        if (success)
        {
            // this.Text = Path.GetFileName(path) + " - ReoGrid Editor " + this.ProductVersion;
            //showGridLinesToolStripMenuItem.Checked = worksheet.HasSettings(WorksheetSettings.View_ShowGuideLine);
            ShowStatus(String.Empty);
            CurrentFilePath = path;
            currentTempFilePath = null;

#if EX_SCRIPT
				// check whether grid contains any scripts
				if (!string.IsNullOrEmpty(this.grid.Script))
				{
					if (MessageBox.Show(LangResource.Msg_Load_Script_Prompt,
						LangResource.Msg_Load_Script_Prompt_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						if (scriptEditor == null || scriptEditor.IsDisposed)
						{
							scriptEditor = new ReoScriptEditor()
							{
								Script = this.grid.Script,
								Srm = this.grid.Srm,
							};
						}

						// run init script
						this.grid.RunScript();

						// show script editor window
						if (!scriptEditor.Visible)
						{
							scriptEditor.Show();
						}
					}
				}
#endif
        }
    }


}