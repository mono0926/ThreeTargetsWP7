using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ThreeTargets
{
    public enum Contract
    {
        None,
        InitializeFail,
        InitializeSuccess,
        ChangeCurrentManifest,
        //EditManifest,
        AddDesc,
        SaveManifest,
        UpdateDetail,
        UpdateMain,
        SaveSetting,
        LoadSetting,
        RedirectToInitial,
        SettingToHome,
        ClearInitial,
        UpdateAgent,
    }
}