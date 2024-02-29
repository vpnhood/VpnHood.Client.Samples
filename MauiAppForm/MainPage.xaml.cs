using VpnHood.Client.App;

namespace VpnHood.Client.Samples.MauiAppForm;

// ReSharper disable once RedundantExtendsListEntry
public partial class MainPage : ContentPage
{
    private static AppConnectionState ConnectionState => VpnHoodApp.Instance.State.ConnectionState;

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
            // disconnect if already connected
            if (ConnectionState is AppConnectionState.Connecting or AppConnectionState.Connected)
            {
                await VpnHoodApp.Instance.Disconnect();
                return;
            }

            // Connect
            await VpnHoodApp.Instance.Connect();
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
            if (ConnectionState is AppConnectionState.None)
            {
                CounterBtn.Text = "Connect";
                StatusLabel.Text = "Disconnected";
            }
            else if (ConnectionState is AppConnectionState.Connecting)
            {
                CounterBtn.Text = "Disconnect";
                StatusLabel.Text = "Connecting";
            }
            else if (ConnectionState is AppConnectionState.Connected)
            {
                CounterBtn.Text = "Disconnect";
                StatusLabel.Text = "Connected";
            }
        });
    }
}