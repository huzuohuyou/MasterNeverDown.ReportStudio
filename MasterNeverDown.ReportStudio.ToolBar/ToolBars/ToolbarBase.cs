// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.IO;
using System.Resources;
using System.Windows.Forms;
using MasterNeverDown.ReportStudio.ToolBar.LangRes;
using unvell.ReoGrid;
using unvell.ReoGrid.Actions;
using unvell.ReoGrid.IO;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars;

public abstract class ToolbarBase(ReoGridControl grid) : ICanUpdateDoc
{
    protected string? CurrentFilePath { get; set; }
    protected string? currentTempFilePath;
    protected readonly ReoGridControl grid = grid;
    private readonly ResXResourceSet resxSet = new($"{AppDomain.CurrentDomain.BaseDirectory}LangRes\\LangResource.resx");

    protected ReoGridControl GridControl { get { return grid; } }

    protected Worksheet CurrentWorksheet { get { return grid.CurrentWorksheet; } }

    protected RangePosition CurrentSelectionRange
    {
        get { return grid.CurrentWorksheet.SelectionRange; }
        set { grid.CurrentWorksheet.SelectionRange = value; }
    }
    public abstract void Execute(params object[] param);
   
    internal void ShowStatus(string msg)
    {
        ShowStatus(msg, false);
    }
    internal void ShowStatus(string msg, bool error)
    {
        // statusToolStripStatusLabel.Text = msg;
        // statusToolStripStatusLabel.ForeColor = error ? Color.Red : SystemColors.WindowText;
    }
    public void ShowError(string msg)
    {
        ShowStatus(msg, true);
    }

    protected void SetCurrentDocumentFile(string? filepath)
    {
        // this.Text = System.IO.Path.GetFileName(filepath) + " - ReoGrid Editor " + this.ProductVersion;
        CurrentFilePath = filepath;
        currentTempFilePath = null;
    }

    public void SaveFile(string? path)
    {
#if EX_SCRIPT
			if (scriptEditor != null && scriptEditor.Visible)
			{
				this.grid.Script = scriptEditor.Script;
			}
#endif // EX_SCRIPT

        //, "ReoGridEditor " + this.ProductVersion.ToString())
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

            SetCurrentDocumentFile(path);

#if DEBUG
            // RGUtility.OpenFileOrLink(path);
#endif
        }
        catch (Exception ex)
        {
            MessageBox.Show(null, LangResource.Save_Error + ex.Message, LangResource.Save_Workbook,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    private FileFormat GetFormatByExtension(string? path)
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

    protected void SaveDocument()
    {
        if (String.IsNullOrEmpty(CurrentFilePath))
        {
            SaveAsDocument();
        }
        else
        {
            SaveFile(CurrentFilePath);
        }
    }

    protected void SetSelectionBorder(BorderPositions borderPos, BorderLineStyle style)
    {
        grid.DoAction(new SetRangeBorderAction(CurrentWorksheet.SelectionRange, borderPos,
            new RangeBorderStyle { Color = new unvell.ReoGrid.Graphics.SolidColor(0,0,0),Style = style }));
    }

    protected bool SaveAsDocument()
    {
        using var sfd = new SaveFileDialog();
        sfd.Filter =resxSet.GetString("Filter_Save_File");

        if (!String.IsNullOrEmpty(CurrentFilePath))
        {
            sfd.FileName = Path.GetFileNameWithoutExtension(CurrentFilePath);

            FileFormat format = GetFormatByExtension(CurrentFilePath);

            switch (format)
            {
                case FileFormat.Excel2007:
                    sfd.FilterIndex = 1;
                    break;

                case FileFormat.ReoGridFormat:
                    sfd.FilterIndex = 2;
                    break;

                case FileFormat.CSV:
                    sfd.FilterIndex = 3;
                    break;
            }
        }

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            SaveFile(sfd.FileName);
            return true;
        }

        return false;
    }
    
    
}