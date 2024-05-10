using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Service.QuickSettings;
using Android.Views;
using VpnHood.Client.Device.Droid;

namespace VpnHood.Client.Samples.MauiCoreForm;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    Exported = true,
    WindowSoftInputMode = SoftInput.AdjustResize, // resize app when keyboard is shown
    AlwaysRetainTaskState = true,
    LaunchMode = LaunchMode.SingleInstance,
    ScreenOrientation = ScreenOrientation.Unspecified,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.LayoutDirection |
                           ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.FontScale |
                           ConfigChanges.Locale | ConfigChanges.Navigation | ConfigChanges.UiMode)]

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
