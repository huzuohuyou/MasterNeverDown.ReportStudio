// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Reflection;

using ReportEditor.ViewModels;
using Wpf.Ui.Controls;

namespace ReportEditor.Views;
/// <summary>
/// Interaction logic for EditorControl.xaml
/// </summary>
public partial class EditorControl : INavigableView<EditorControlViewModel>
{
    public EditorControl()
    {
        InitializeComponent();
        

    }

    public EditorControlViewModel ViewModel { get; }
}
