
using VpnHood.Client.App;

namespace VpnHood.Client.Samples.MauiAppForm;
public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);
        window.Width = VpnHoodApp.Instance.Resources.WindowSize.Width;
        window.Height = VpnHoodApp.Instance.Resources.WindowSize.Height;
        window.Title = VpnHoodApp.Instance.Resources.Strings.AppName;
        return window;
    }

    protected override void CleanUp()
    {
        base.CleanUp();
        if (VpnHoodApp.IsInit) _ = VpnHoodApp.Instance.DisposeAsync();
    }
}