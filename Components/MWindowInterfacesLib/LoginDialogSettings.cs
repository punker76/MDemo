namespace MWindowInterfacesLib
{
    using MWindowInterfaceLib.Interfaces.LoginDialog;
    using System.Windows;

    public class LoginDialogSettings : MetroDialogSettings, ILoginDialogSettings
    {
        private const string DefaultUsernameWatermark = "Username...";
        private const string DefaultPasswordWatermark = "Password...";
        private const Visibility DefaultNegativeButtonVisibility = Visibility.Collapsed;
        private const bool DefaultShouldHideUsername = false;
        private const bool DefaultEnablePasswordPreview = false;
        private const Visibility DefaultRememberCheckBoxVisibility = Visibility.Collapsed;
        private const string DefaultRememberCheckBoxText = "Remember";

        public LoginDialogSettings()
        {
            UsernameWatermark = DefaultUsernameWatermark;
            PasswordWatermark = DefaultPasswordWatermark;
            NegativeButtonVisibility = DefaultNegativeButtonVisibility;
            ShouldHideUsername = DefaultShouldHideUsername;
            AffirmativeButtonText = "Login";
            EnablePasswordPreview = DefaultEnablePasswordPreview;
            RememberCheckBoxVisibility = DefaultRememberCheckBoxVisibility;
            RememberCheckBoxText = DefaultRememberCheckBoxText;
        }

        public string InitialUsername { get; set; }

        public string InitialPassword { get; set; }

        public string UsernameWatermark { get; set; }

        public bool ShouldHideUsername { get; set; }

        public string PasswordWatermark { get; set; }

        public Visibility NegativeButtonVisibility { get; set; }

        public bool EnablePasswordPreview { get; set; }

        public Visibility RememberCheckBoxVisibility { get; set; }

        public string RememberCheckBoxText { get; set; }
    }
}
