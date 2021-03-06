﻿namespace MWindowDialogLib.Dialogs
{
    using Internal;
    using MWindowInterfaceLib.Interfaces.LoginDialog;
    using MWindowInterfacesLib;
    using MWindowInterfacesLib.Enums;
    using MWindowInterfacesLib.Interfaces;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : BaseMetroDialog
    {
        internal LoginDialog(IMetroWindow parentWindow)
            : this(parentWindow, null)
        {
        }

        internal LoginDialog(IMetroWindow parentWindow
                           , ILoginDialogSettings settings)
            : base(parentWindow, settings as IMetroDialogSettings)
        {
            InitializeComponent();
            Username = settings.InitialUsername;
            Password = settings.InitialPassword;
            UsernameWatermark = settings.UsernameWatermark;
            PasswordWatermark = settings.PasswordWatermark;
            NegativeButtonButtonVisibility = settings.NegativeButtonVisibility;
            ShouldHideUsername = settings.ShouldHideUsername;
            RememberCheckBoxVisibility = settings.RememberCheckBoxVisibility;
            RememberCheckBoxText = settings.RememberCheckBoxText;
        }

        internal Task<ILoginDialogData> WaitForButtonPressAsync()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Focus();
                if (string.IsNullOrEmpty(this.PART_TextBox.Text) && !ShouldHideUsername)
                {
                    this.PART_TextBox.Focus();
                }
                else
                {
                    this.PART_TextBox2.Focus();
                }
            }));

            TaskCompletionSource<ILoginDialogData> tcs = new TaskCompletionSource<ILoginDialogData>();

            RoutedEventHandler negativeHandler = null;
            KeyEventHandler negativeKeyHandler = null;

            RoutedEventHandler affirmativeHandler = null;
            KeyEventHandler affirmativeKeyHandler = null;

            KeyEventHandler escapeKeyHandler = null;

            Action cleanUpHandlers = null;

            var cancellationTokenRegistration = DialogSettings.CancellationToken.Register(() =>
            {
                cleanUpHandlers();
                tcs.TrySetResult(null);
            });

            cleanUpHandlers = () => {
                PART_TextBox.KeyDown -= affirmativeKeyHandler;
                PART_TextBox2.KeyDown -= affirmativeKeyHandler;

                this.KeyDown -= escapeKeyHandler;

                PART_NegativeButton.Click -= negativeHandler;
                PART_AffirmativeButton.Click -= affirmativeHandler;

                PART_NegativeButton.KeyDown -= negativeKeyHandler;
                PART_AffirmativeButton.KeyDown -= affirmativeKeyHandler;

                cancellationTokenRegistration.Dispose();
            };

            escapeKeyHandler = (sender, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(null);
                }
            };

            negativeKeyHandler = (sender, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    cleanUpHandlers();

                    tcs.TrySetResult(null);
                }
            };

            affirmativeKeyHandler = (sender, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    cleanUpHandlers();
                    tcs.TrySetResult(new LoginDialogData { Username = Username, Password = PART_TextBox2.Password, ShouldRemember = RememberCheckBoxChecked });
                }
            };

            negativeHandler = (sender, e) =>
            {
                cleanUpHandlers();

                tcs.TrySetResult(null);

                e.Handled = true;
            };

            affirmativeHandler = (sender, e) =>
            {
                cleanUpHandlers();

                tcs.TrySetResult(new LoginDialogData { Username = Username, Password = PART_TextBox2.Password, ShouldRemember = RememberCheckBoxChecked });

                e.Handled = true;
            };

            PART_NegativeButton.KeyDown += negativeKeyHandler;
            PART_AffirmativeButton.KeyDown += affirmativeKeyHandler;

            PART_TextBox.KeyDown += affirmativeKeyHandler;
            PART_TextBox2.KeyDown += affirmativeKeyHandler;

            this.KeyDown += escapeKeyHandler;

            PART_NegativeButton.Click += negativeHandler;
            PART_AffirmativeButton.Click += affirmativeHandler;

            return tcs.Task;
        }

        protected override void OnLoaded()
        {
            var settings = this.DialogSettings as LoginDialogSettings;

////            if (settings != null && settings.EnablePasswordPreview)
////            {
////                var win8MetroPasswordStyle = this.FindResource("Win8MetroPasswordBox") as Style;
////                if (win8MetroPasswordStyle != null)
////                {
////                    PART_TextBox2.Style = win8MetroPasswordStyle;
////                }
////            }

            this.AffirmativeButtonText = this.DialogSettings.AffirmativeButtonText;
            this.NegativeButtonText = this.DialogSettings.NegativeButtonText;

////            switch (this.DialogSettings.ColorScheme)
////            {
////                case MetroDialogColorScheme.Accented:
////                    this.PART_NegativeButton.Style = this.FindResource("AccentedDialogHighlightedSquareButton") as Style;
////            PART_TextBox.SetResourceReference(ForegroundProperty, "BlackColorBrush");
////                    PART_TextBox2.SetResourceReference(ForegroundProperty, "BlackColorBrush");
////                    break;
////            }
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(LoginDialog), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register("Username", typeof(string), typeof(LoginDialog), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty UsernameWatermarkProperty = DependencyProperty.Register("UsernameWatermark", typeof(string), typeof(LoginDialog), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(LoginDialog), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty PasswordWatermarkProperty = DependencyProperty.Register("PasswordWatermark", typeof(string), typeof(LoginDialog), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty AffirmativeButtonTextProperty = DependencyProperty.Register("AffirmativeButtonText", typeof(string), typeof(LoginDialog), new PropertyMetadata("OK"));
        public static readonly DependencyProperty NegativeButtonTextProperty = DependencyProperty.Register("NegativeButtonText", typeof(string), typeof(LoginDialog), new PropertyMetadata("Cancel"));
        public static readonly DependencyProperty NegativeButtonButtonVisibilityProperty = DependencyProperty.Register("NegativeButtonButtonVisibility", typeof(Visibility), typeof(LoginDialog), new PropertyMetadata(Visibility.Collapsed));
        public static readonly DependencyProperty ShouldHideUsernameProperty = DependencyProperty.Register("ShouldHideUsername", typeof(bool), typeof(LoginDialog), new PropertyMetadata(false));
        public static readonly DependencyProperty RememberCheckBoxVisibilityProperty = DependencyProperty.Register("RememberCheckBoxVisibility", typeof(Visibility), typeof(LoginDialog), new PropertyMetadata(Visibility.Collapsed));
        public static readonly DependencyProperty RememberCheckBoxTextProperty = DependencyProperty.Register("RememberCheckBoxText", typeof(string), typeof(LoginDialog), new PropertyMetadata("Remember"));
        public static readonly DependencyProperty RememberCheckBoxCheckedProperty = DependencyProperty.Register("RememberCheckBoxChecked", typeof(bool), typeof(LoginDialog), new PropertyMetadata(false));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public string UsernameWatermark
        {
            get { return (string)GetValue(UsernameWatermarkProperty); }
            set { SetValue(UsernameWatermarkProperty, value); }
        }

        public string PasswordWatermark
        {
            get { return (string)GetValue(PasswordWatermarkProperty); }
            set { SetValue(PasswordWatermarkProperty, value); }
        }

        public string AffirmativeButtonText
        {
            get { return (string)GetValue(AffirmativeButtonTextProperty); }
            set { SetValue(AffirmativeButtonTextProperty, value); }
        }

        public string NegativeButtonText
        {
            get { return (string)GetValue(NegativeButtonTextProperty); }
            set { SetValue(NegativeButtonTextProperty, value); }
        }

        public Visibility NegativeButtonButtonVisibility
        {
            get { return (Visibility)GetValue(NegativeButtonButtonVisibilityProperty); }
            set { SetValue(NegativeButtonButtonVisibilityProperty, value); }
        }

        public bool ShouldHideUsername
        {
            get { return (bool)GetValue(ShouldHideUsernameProperty); }
            set { SetValue(ShouldHideUsernameProperty, value); }
        }

        public Visibility RememberCheckBoxVisibility
        {
            get { return (Visibility)GetValue(RememberCheckBoxVisibilityProperty); }
            set { SetValue(RememberCheckBoxVisibilityProperty, value); }
        }

        public string RememberCheckBoxText
        {
            get { return (string)GetValue(RememberCheckBoxTextProperty); }
            set { SetValue(RememberCheckBoxTextProperty, value); }
        }

        public bool RememberCheckBoxChecked
        {
            get { return (bool)GetValue(RememberCheckBoxCheckedProperty); }
            set { SetValue(RememberCheckBoxCheckedProperty, value); }
        }
    }
}
