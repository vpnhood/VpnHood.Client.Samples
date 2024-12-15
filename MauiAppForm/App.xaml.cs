
using VpnHood.AppLib;

namespace VpnHood.Client.Samples.MauiAppForm;
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell())
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
        if (VpnHoodApp.IsInit) _ = VpnHoodApp.Instance.DisposeAsync();
    }
}