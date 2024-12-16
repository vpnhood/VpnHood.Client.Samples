using Android.Graphics;
using VpnHood.Core.Client;
using VpnHood.Core.Client.Device.Droid;
using VpnHood.Core.Client.Device.Droid.ActivityEvents;
using VpnHood.Core.Common.Tokens;

// ReSharper disable StringLiteralTypo
namespace VpnHood.App.CoreSample.AndroidForm;

[Activity(Label = "@string/app_name", MainLauncher = true)]
// ReSharper disable once UnusedMember.Global
public class MainActivity : ActivityEvent
{
    private static readonly AndroidDevice Device = AndroidDevice.Create();
    private VpnHoodClient? _vpnHoodClient;
    private Button _connectButton = default!;
    private TextView _statusTextView = default!;

    private bool IsConnectingOrConnected => _vpnHoodClient?.State is ClientState.Connecting or ClientState.Connected;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

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
        var note = new TextView(this);
        note.SetPadding(note.PaddingLeft, note.PaddingTop + 30, note.PaddingRight, note.PaddingBottom);
        note.SetTextColor(Color.Blue);
        note.Text = "This sample demonstrates how to connect to a VpnHoodServer using the VpnHoodClient (core). However, developing a fully functional VPN application involves much more, including handling UI commands, accounting, billing, advertising, notifications, managing keys, handling reconnections, handling exceptions, acquiring permissions, leveraging OS features such as Tile, Always ON, among other considerations. Consider using VpnHood.AppLib, which provides a comprehensive set of extended functionalities.";
        mainView.AddView(note);

        note = new TextView(this);
        note.SetPadding(note.PaddingLeft, note.PaddingTop + 30, note.PaddingRight, note.PaddingBottom);
        note.SetTextColor(Color.IndianRed);
        note.Text = "* This is a test server, and the session will be terminated in a few minutes.";
        mainView.AddView(note);

        note = new TextView(this);
        note.SetPadding(note.PaddingLeft, note.PaddingTop + 30, note.PaddingRight, note.PaddingBottom);
        note.SetTextColor(Color.Black);
        note.Text = "* Please contact us if this sample is outdated or not working. Report issues at: https://github.com/vpnhood/VpnHood/issues";
        mainView.AddView(note);

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

            // a unique id of your client
            var clientId = Guid.Parse("7BD6C156-EEA3-43D5-90AF-B118FE47ED0B").ToString();
            const string accessKey = ClientOptions.SampleAccessKey; // This is for test purpose only and can not be used in production
            var token = Token.FromAccessKey(accessKey);
            var packetCapture = await Device.CreatePacketCapture(new AndroidUiContext(this));

            // Connect
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