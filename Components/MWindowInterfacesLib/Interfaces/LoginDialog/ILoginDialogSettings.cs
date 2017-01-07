namespace MWindowInterfaceLib.Interfaces.LoginDialog
{
    using MWindowInterfacesLib.Interfaces;
    using System.Windows;

    public interface ILoginDialogSettings : IMetroDialogSettings
    {
        bool EnablePasswordPreview { get; set; }
        string InitialPassword { get; set; }
        string InitialUsername { get; set; }
        Visibility NegativeButtonVisibility { get; set; }
        string PasswordWatermark { get; set; }
        string RememberCheckBoxText { get; set; }
        Visibility RememberCheckBoxVisibility { get; set; }
        bool ShouldHideUsername { get; set; }
        string UsernameWatermark { get; set; }
    }
}