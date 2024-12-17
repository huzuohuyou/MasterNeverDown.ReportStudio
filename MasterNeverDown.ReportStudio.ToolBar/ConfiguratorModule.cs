// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using CommunityToolkit.ReportDesigner.ToolBars.Impl;
using Microsoft.Extensions.DependencyInjection;
using ReportEditor.ToolBars;


namespace ReportEditor;

[SeCommunityToolkitModule]
public static class ConfiguratorModule 
{
    // public void AddModule(ObservableCollection<object> menuItems)
    // {
    //     menuItems.Add(new Wpf.Ui.Controls.NavigationViewItem("CommunityToolkit.ReportDesigner", Wpf.Ui.Controls.SymbolRegular.Edit24,
    //         typeof(EditorControl)));
    // }

    public static void AddService(IServiceCollection services)
    {
        services.AddTransientFromNamespace("CommunityToolkit.ReportDesigner.Views", ConfiguratorAssembly.Asssembly);
        services.AddTransientFromNamespace("CommunityToolkit.ReportDesigner.ViewModels", ConfiguratorAssembly.Asssembly);
        services.AddKeyedSingleton<ICanUpdateDoc, MergeSelectionRange>("MergeSelectionRange");
    }
}