using VpnHood.AppLib;
using VpnHood.AppLib.WebServer;

namespace VpnHood.Client.Samples.MauiAppSpa;

public partial class App
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new MainPage())
        {
            Width = VpnHoodApp.Instance.Resource.WindowSize.Width,
            Height = VpnHoodApp.Instance.Resource.WindowSize.Height,
            Title = VpnHoodApp.Instance.Resource.Strings.AppName
        };
        return window;
    }

    protected override void CleanUp()
    {
        base.CleanUp();
        if (VpnHoodAppWebServer.IsInit) VpnHoodAppWebServer.Instance.Dispose();
        if (VpnHoodApp.IsInit) _ = VpnHoodApp.Instance.DisposeAsync();
    }
}