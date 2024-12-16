using Android.App;
using Android.OS;
using Android.Service.QuickSettings;
using VpnHood.AppLib.Droid.Common.Constants;
using VpnHood.AppLib.Maui.Common;
using VpnHood.Core.Client.Device.Droid;

namespace VpnHood.App.CoreSample.MauiForm;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    LaunchMode = AndroidMainActivityConstants.LaunchMode,
    Exported = AndroidMainActivityConstants.Exported,
    WindowSoftInputMode = AndroidMainActivityConstants.WindowSoftInputMode,
    ScreenOrientation = AndroidMainActivityConstants.ScreenOrientation,
    ConfigurationChanges = AndroidMainActivityConstants.ConfigChanges)]


[IntentFilter([TileService.ActionQsTilePreferences])]
public class MainActivity : MauiActivityEvent
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        MauiProgram.CurrentUiContext = new AndroidUiContext(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MauiProgram.CurrentUiContext = null;
    }
}
