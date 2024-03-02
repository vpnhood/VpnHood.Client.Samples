using Microsoft.UI.Xaml;
using VpnHood.Client.Device.WinDivert;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

// ReSharper disable once CheckNamespace
namespace VpnHood.Client.Samples.MauiAppSpa.WinUI;


/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
    private readonly VpnHoodMauiWinUiAppHandler _vpnHoodMauiWinUiAppHandler;

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();
        _vpnHoodMauiWinUiAppHandler = new VpnHoodMauiWinUiAppHandler();
    }

    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp(new WinDivertDevice());
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);
        _vpnHoodMauiWinUiAppHandler.OnLaunched(args);
    }
}

