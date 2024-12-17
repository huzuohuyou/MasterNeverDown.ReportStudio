// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Drawing;

namespace MasterNeverDown.ReportStudio.ToolBar.ToolBars;

internal class SolidColor : AbstractColor
{
    private Color color;

    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    public SolidColor(Color color)
    {
        this.color = color;
    }

    public override bool IsEmpty
    {
        get { return color.IsEmpty; }
    }

    public override Color CompliantSolidColor
    {
        get { return color; }
    }

    public override AbstractColor Clone()
    {
        return new SolidColor(color);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is SolidColor)) return false;
        return color.Equals(((SolidColor)obj).color);
    }

    public override int GetHashCode()
    {
        return color.GetHashCode();
    }
}