// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Drawing;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using unvell.ReoGrid;
using unvell.ReoGrid.Chart;

namespace ReportEditor.ViewModels;

delegate string TestDelegate(ReoGridControl grid);

public partial class EditorControlViewModel : ObservableObject
{
    private ReoGridControl grid = null!;


    protected ReoGridControl GridControl { get { return grid; } }

    protected Worksheet CurrentWorksheet { get { return grid.CurrentWorksheet; } }

    protected RangePosition CurrentSelectionRange
    {
        get { return grid.CurrentWorksheet.SelectionRange; }
        set { grid.CurrentWorksheet.SelectionRange = value; }
    }
    private Worksheet worksheet;
    [RelayCommand]
    private void OnLoadedWindow(object sender)
    {
        grid = (ReoGridControl)sender;
        grid.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
        grid.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowColumnHeader, false);
        worksheet = grid.CurrentWorksheet;
        worksheet["A2"] = new object[,] {
            { null, 2008, 2009, 2010, 2011, 2012 },
            { "City 1", 3, 2, 4, 2, 6 },
            { "City 2", 7, 5, 3, 6, 4 },
            { "City 3", 13, 10, 9, 10, 9 },
            { "Total",  "=SUM(B3:B5)", "=SUM(C3:C5)", "=SUM(D3:D5)", "=SUM(E3:E5)", "=SUM(F3:F5)" },};
        var dataRange = worksheet.Ranges["B3:F5"];
        var rowTitleRange = worksheet.Ranges["A3:A6"];
        var categoryNamesRange = worksheet.Ranges["B2:F2"];

        worksheet.AddHighlightRange(rowTitleRange);
        worksheet.AddHighlightRange(categoryNamesRange);
        worksheet.AddHighlightRange(dataRange);

        var chart = new BarChart
        {
            Location = new unvell.ReoGrid.Graphics.Point(220, 160),
            Size = new unvell.ReoGrid.Graphics.Size(400, 260),

            Title = "Bar Chart Sample",

            DataSource = new WorksheetChartDataSource(worksheet, rowTitleRange, dataRange)
            {
                CategoryNameRange = categoryNamesRange,
            },
        };

        worksheet.FloatingObjects.Add(chart);
    }

    private void Do(object sender)
    {
        var ass = Assembly.GetExecutingAssembly();
        var fullName = $"CommunityToolkit.ReportDesigner.ToolBars.Impl.{sender}";
        Type t = ass.GetType(fullName) ?? throw new InvalidOperationException($"不存在 {fullName}");
        var o = Activator.CreateInstance(t, grid);
        MethodInfo mi = t.GetMethod("Excute") ?? throw new InvalidOperationException("不存在Excute");
        mi.Invoke(o, null);
    }


    [RelayCommand]
    private void OnToolBarExecute(object sender) => Do(sender);
}