using Android.Graphics;
using VpnHood.Client.Device.Droid;
using VpnHood.Client.Device.Droid.Utils;
using VpnHood.Common;

// ReSharper disable StringLiteralTypo
namespace VpnHood.Client.Samples.AndroidCoreForm;

[Activity(Label = "@string/app_name", MainLauncher = true)]
// ReSharper disable once UnusedMember.Global
public class MainActivity : ActivityEvent
{
    private static readonly AndroidDevice Device = new();
    private VpnHoodClient? _vpnHoodClient;
    private Button _connectButton = default!;
    private TextView _statusTextView = default!;

    private bool IsConnectingOrConnected => _vpnHoodClient?.State is ClientState.Connecting or ClientState.Connected;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Device.Prepare(this);

        // Set our simple view
        var mainView = new LinearLayout(this);
        mainView.SetPadding(20, 10, 20, 10);
        mainView.Orientation = Orientation.Vertical;

        // button and status
        var linearLayout = new LinearLayout(this);
        _connectButton = new Button(this);
        _connectButton.Click += ConnectButton_Click;
        linearLayout.AddView(_connectButton);

        _statusTextView = new TextView(this);
        _statusTextView.SetTextColor(Color.DarkGreen);
        linearLayout.AddView(_statusTextView);
        mainView.AddView(linearLayout);

        // notes
        var note1 = new TextView(this);
        note1.SetPadding(note1.PaddingLeft, note1.PaddingTop + 30, note1.PaddingRight, note1.PaddingBottom);
        note1.SetTextColor(Color.IndianRed);
        note1.Text = "This sample demonstrates how to connect to a VpnHoodServer using the VpnHoodClient. However, developing a fully functional VPN application involves much more, including handling UI commands, accounting, billing, advertising, notifications, managing keys, handling reconnections, handling exceptions, acquiring permissions, leveraging OS features such as Tile, Always ON, among other considerations. Consider using VpnHood.Client.App, which provides a comprehensive set of extended functionalities.";
        mainView.AddView(note1);

        var note2 = new TextView(this);
        note2.SetPadding(note2.PaddingLeft, note2.PaddingTop + 30, note2.PaddingRight, note2.PaddingBottom);
        note2.SetTextColor(Color.Black);
        note2.Text = "* Please contact us if this sample is outdated or not working. Report issues at: https://github.com/vpnhood/VpnHood/issues";
        mainView.AddView(note2);

        SetContentView(mainView);
        UpdateUi();
    }

    private void ConnectButton_Click(object? sender, EventArgs e)
    {
        Task.Run(ConnectTask);
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
            // accessKey must obtain from the server
            var clientId = Guid.Parse("7BD6C156-EEA3-43D5-90AF-B118FE47ED0B");
            var accessKey = "vh://eyJuYW1lIjoiVnBuSG9vZCBQdWJsaWMgU2VydmVycyIsInYiOjEsInNpZCI6MTAwMSwidGlkIjoiNWFhY2VjNTUtNWNhYy00NTdhLWFjYWQtMzk3Njk2OTIzNmY4Iiwic2VjIjoiNXcraUhNZXcwQTAzZ3c0blNnRFAwZz09IiwiaXN2IjpmYWxzZSwiaG5hbWUiOiJtby5naXdvd3l2eS5uZXQiLCJocG9ydCI6NDQzLCJjaCI6IjNnWE9IZTVlY3VpQzlxK3NiTzdobExva1FiQT0iLCJwYiI6dHJ1ZSwidXJsIjoiaHR0cHM6Ly93d3cuZHJvcGJveC5jb20vcy82YWlrdHFmM2xhZW9vaGY/ZGw9MSIsImVwIjpbIjUxLjgxLjIxMC4xNjQ6NDQzIl19";
            var token = Token.FromAccessKey(accessKey);
            var packetCapture = await Device.CreatePacketCapture();

            _vpnHoodClient = new VpnHoodClient(packetCapture, clientId, token, new ClientOptions());
            _vpnHoodClient.StateChanged += (_, _) => UpdateUi();
            await _vpnHoodClient.Connect();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task Disconnect()
    {
        if (_vpnHoodClient != null)
            await _vpnHoodClient.DisposeAsync();
        _vpnHoodClient = null;
    }

    private void UpdateUi()
    {
        RunOnUiThread(() =>
        {
            _statusTextView.Text = _vpnHoodClient?.State.ToString() ?? "Disconnected";
            _connectButton.Text = _vpnHoodClient == null || _vpnHoodClient?.State is ClientState.None or ClientState.Disposed ? "Connect" : "Disconnect";
        });
    }
}