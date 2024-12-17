// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Drawing;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars;

public abstract class AbstractColor
{
    public abstract bool IsEmpty { get; }

    public abstract AbstractColor Clone();

    public abstract Color CompliantSolidColor { get; }
}