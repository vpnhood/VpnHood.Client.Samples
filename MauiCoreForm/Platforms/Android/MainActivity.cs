using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Service.QuickSettings;
using Android.Views;
using VpnHood.Client.Device.Droid;
using VpnHood.Client.Device.Droid.Utils;

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
public class MainActivity : MauiAppCompatActivity, IActivityEvent
{
    public event EventHandler<ActivityResultEventArgs>? OnActivityResultEvent;
    public event EventHandler? OnDestroyEvent;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        ((AndroidDevice)MauiProgram.VpnHoodDevice).Prepare(this);
    }

    protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent? data)
    {
        OnActivityResultEvent?.Invoke(this, new ActivityResultEventArgs
        {
            RequestCode = requestCode,
            ResultCode = resultCode,
            Data = data
        });

        base.OnActivityResult(requestCode, resultCode, data);
    }
    protected override void OnDestroy()
    {
        OnDestroyEvent?.Invoke(this, EventArgs.Empty);
        base.OnDestroy();
    }

}
