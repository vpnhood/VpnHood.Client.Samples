using VpnHood.Common;

namespace VpnHood.Client.Samples.MauiCoreForm;

// ReSharper disable once RedundantExtendsListEntry
public partial class MainPage : ContentPage
{
    private VpnHoodClient? _vpnHoodClient;

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
            if (_vpnHoodClient?.State is ClientState.Connecting or ClientState.Connected)
            {
                await Disconnect();
                return;
            }

            // Connect
            // accessKey must obtain from the server
            var clientId = Guid.Parse("7BD6C156-EEA3-43D5-90AF-B118FE47ED0B");
            var accessKey = "vh://eyJuYW1lIjoiVnBuSG9vZCBQdWJsaWMgU2VydmVycyIsInYiOjEsInNpZCI6MTAwMSwidGlkIjoiNWFhY2VjNTUtNWNhYy00NTdhLWFjYWQtMzk3Njk2OTIzNmY4Iiwic2VjIjoiNXcraUhNZXcwQTAzZ3c0blNnRFAwZz09IiwiaXN2IjpmYWxzZSwiaG5hbWUiOiJtby5naXdvd3l2eS5uZXQiLCJocG9ydCI6NDQzLCJjaCI6IjNnWE9IZTVlY3VpQzlxK3NiTzdobExva1FiQT0iLCJwYiI6dHJ1ZSwidXJsIjoiaHR0cHM6Ly93d3cuZHJvcGJveC5jb20vcy82YWlrdHFmM2xhZW9vaGY/ZGw9MSIsImVwIjpbIjUxLjgxLjIxMC4xNjQ6NDQzIl19";
            var token = Token.FromAccessKey(accessKey);
            var packetCapture = await MauiProgram.VpnHoodDevice.CreatePacketCapture();

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