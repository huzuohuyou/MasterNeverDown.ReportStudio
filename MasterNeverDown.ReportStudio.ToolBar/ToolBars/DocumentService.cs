// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using MasterNeverDown.ReportStudio.ToolBar.ToolBars.Impl;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars;

public class DocumentService
{
    public DocumentService(NewFile fileService)
    {
        _fileService = fileService;
    }

    private NewFile _fileService { get; set; }
}