﻿using Android.App;
using Android.Runtime;

namespace VpnHood.App.AppLibSample.MauiForm;


[Application]
public class MainApplication(IntPtr handle, JniHandleOwnership ownership)
    : MauiApplication(handle, ownership)
{
    protected override MauiApp CreateMauiApp()
    {
        return MauiProgram.CreateMauiApp();
    }
}