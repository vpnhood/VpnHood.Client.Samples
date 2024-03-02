using Android.App;
using Android.Runtime;
using VpnHood.Client.Device.Droid;

namespace VpnHood.Client.Samples.MauiCoreForm;


[Application]
public class MainApplication(IntPtr handle, JniHandleOwnership ownership) 
    : MauiApplication(handle, ownership)
{

    protected override MauiApp CreateMauiApp()
    {
        var mauiApp = MauiProgram.CreateMauiApp(AndroidDevice.Create());
        return mauiApp;
    }
}