using Android.App;
using Android.Content.PM;
using Android.Service.QuickSettings;
using Android.Views;
using VpnHood.AppLib.Droid.Common.Activities;
using VpnHood.AppLib.Maui.Common;

namespace VpnHood.Client.Samples.MauiAppForm;

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
public class MainActivity : VpnHoodMauiMainActivity
{
    protected override AndroidAppMainActivityHandler CreateMainActivityHandler()
    {
        return new AndroidAppMainActivityHandler(this, new AndroidMainActivityOptions
        {
        });
    }
}
