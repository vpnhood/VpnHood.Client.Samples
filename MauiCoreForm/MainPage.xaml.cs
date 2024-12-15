using VpnHood.Core.Client;
using VpnHood.Core.Common.Tokens;

namespace VpnHood.Client.Samples.MauiCoreForm;

// ReSharper disable StringLiteralTypo
// ReSharper disable once RedundantExtendsListEntry
public partial class MainPage : ContentPage
{
    private VpnHoodClient? _vpnHoodClient;
    private bool IsConnectingOrConnected => _vpnHoodClient?.State is ClientState.Connecting or ClientState.Connected;

    public MainPage()
    {
        InitializeComponent();
        UpdateUi();
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
            if (IsConnectingOrConnected)
            {
                await Disconnect();
                return;
            }

            // Connect
            var clientId = Guid.Parse("7BD6C156-EEA3-43D5-90AF-B118FE47ED0B").ToString();
            const string accessKey = ClientOptions.SampleAccessKey; // This is for test purpose only and can not be used in production
            var token = Token.FromAccessKey(accessKey);
            var packetCapture = await MauiProgram.VpnHoodDevice.CreatePacketCapture(MauiProgram.CurrentUiContext);

            _vpnHoodClient = new VpnHoodClient(packetCapture, clientId, token, new ClientOptions());
            _vpnHoodClient.StateChanged += (_, _) => UpdateUi();
            await _vpnHoodClient.Connect();
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
            StatusLabel.Text = _vpnHoodClient?.State.ToString() ?? "Disconnected";
            CounterBtn.Text = _vpnHoodClient == null || _vpnHoodClient?.State is ClientState.None or ClientState.Disposed ? "Connect" : "Disconnect";
        });
    }
    private async Task Disconnect()
    {
        if (_vpnHoodClient != null)
            await _vpnHoodClient.DisposeAsync();
        _vpnHoodClient = null;
        UpdateUi();
    }
}