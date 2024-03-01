using VpnHood.Client.App;

namespace VpnHood.Client.Samples.MauiAppForm;

// ReSharper disable once RedundantExtendsListEntry
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        UpdateUi();
        VpnHoodApp.Instance.ConnectionStateChanged += (_, _) => UpdateUi();
    }

    private void OnConnectClicked(object sender, EventArgs e)
    {
        _ = ConnectTask();
    }

    private async Task ConnectTask()
    {
        try
        {
            if (VpnHoodApp.Instance.State.CanConnect)
                await VpnHoodApp.Instance.Connect();

            else if (VpnHoodApp.Instance.State.CanDisconnect)
                await VpnHoodApp.Instance.Disconnect(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void UpdateUi()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            StatusLabel.Text = VpnHoodApp.Instance.State.ConnectionState.ToString();
            CounterBtn.IsEnabled = VpnHoodApp.Instance.State.CanConnect || VpnHoodApp.Instance.State.CanDisconnect;
            CounterBtn.Text = VpnHoodApp.Instance.State.CanConnect ? "Connect" : "Disconnect";
        });
    }
}