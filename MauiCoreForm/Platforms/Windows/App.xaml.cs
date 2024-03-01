﻿using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;
using VpnHood.Client.Device.WinDivert;
using XmlElement = Windows.Data.Xml.Dom.XmlElement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

// ReSharper disable once CheckNamespace
namespace VpnHood.Client.Samples.MauiCoreForm.WinUI;

using Microsoft.Maui.Platform;
using Microsoft.UI.Windowing;
using System.Diagnostics;
using Windows.UI.Notifications;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
// ReSharper disable once RedundantExtendsListEntry
public partial class App : MauiWinUIApplication
{
    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp(new WinDivertDevice());
    }

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();
    }
    
}
