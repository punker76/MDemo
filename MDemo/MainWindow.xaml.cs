namespace MDemo
{
    using MWindowDialogLib;
    using MWindowInterfaceLib.Interfaces.LoginDialog;
    using MWindowInterfacesLib;
    using MWindowInterfacesLib.Enums;
    using MWindowInterfacesLib.Events;
    using MWindowInterfacesLib.Interfaces;
    using MWindowLib.Controls;
    using ServiceLocator;
    using Settings.UserProfile;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MWindowLib.MetroWindow
                                     ,IViewSize  // Implements saving and loading/repositioning of Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        #region Overlay Demo
        #region Message Dialogs
        private async void ShowMessageDialog(object sender, RoutedEventArgs e)
        {
            // This demo runs on .Net 4.0, but we're using the Microsoft.Bcl.Async package so we have async/await support
            // The package is only used by the demo and not a dependency of the library!
            var mySettings = new MWindowInterfacesLib.MetroDialogSettings()
            {
                AffirmativeButtonText = "Hi",
                NegativeButtonText = "Go away!",
                FirstAuxiliaryButtonText = "Cancel",
                ColorScheme = MetroDialogOptions.ColorScheme
            };

            var manager = ServiceContainer.Instance.GetService<IContentDialogService>().Manager;

            MessageDialogResult result = await manager.ShowMessageAsync(this, "Hello!", "Welcome to the world of metro!",
                MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, mySettings);

            if (result != MessageDialogResult.FirstAuxiliary)
                await manager.ShowMessageAsync(this, "Result", "You said: " + (result == MessageDialogResult.Affirmative ? mySettings.AffirmativeButtonText : mySettings.NegativeButtonText +
                    Environment.NewLine + Environment.NewLine + "This dialog will follow the Use Accent setting."));
        }


        private async void ShowLimitedMessageDialog(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Hi",
                NegativeButtonText = "Go away!",
                FirstAuxiliaryButtonText = "Cancel",
                MaximumBodyHeight = 100,
                ColorScheme = MetroDialogOptions.ColorScheme
            };

            var dlg = ServiceContainer.Instance.GetService<IContentDialogService>();

            MessageDialogResult result = await dlg.Manager.ShowMessageAsync(this, "Hello!", "Welcome to the world of metro!" + string.Join(Environment.NewLine, "abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx", "yz"),
                MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, mySettings);

            if (result != MessageDialogResult.FirstAuxiliary)
                await dlg.Manager.ShowMessageAsync(this, "Result", "You said: " + (result == MessageDialogResult.Affirmative ? mySettings.AffirmativeButtonText : mySettings.NegativeButtonText +
                    Environment.NewLine + Environment.NewLine + "This dialog will follow the Use Accent setting."));
        }
        #endregion Message Dialogs

        private async void ShowCustomDialog(object sender, RoutedEventArgs e)
        {
            var dialog = (MWindowDialogLib.Dialogs.BaseMetroDialog)this.Resources["CustomDialogTest"];

            var dlg = ServiceContainer.Instance.GetService<IContentDialogService>();
            await dlg.Manager.ShowMetroDialogAsync(this, dialog);

            var textBlock = dialog.FindChild<TextBlock>("MessageTextBlock");

            // This code fails if you forgot to include the BaseMetroDialog.xaml in Generics.xaml
            textBlock.Text = "A message box will appear in 5 seconds.";

            await Delay(5000);

            await dlg.Manager.ShowMessageAsync(this, "Secondary dialog", "This message is shown on top of another.");

            textBlock.Text = "The dialog will close in 2 seconds.";
            await Delay(2000);

            await dlg.Manager.HideMetroDialogAsync(this, dialog);
        }

        #region Input Dialog
        private async void ShowInputDialog(object sender, RoutedEventArgs e)
        {
            var dlg = ServiceContainer.Instance.GetService<IContentDialogService>();
            var result = await dlg.Manager.ShowInputAsync(this, "Hello!", "What is your name?");

            if (result == null) //user pressed cancel
                return;

            await dlg.Manager.ShowMessageAsync(this, "Hello", "Hello " + result + "!");
        }
        #endregion Input Dialog

        #region LoginDialog
        private async void ShowLoginDialog(object sender, RoutedEventArgs e)
        {
            var dlg = ServiceContainer.Instance.GetService<IContentDialogService>();

            ILoginDialogData result = await dlg.Manager.ShowLoginAsync(
                            this, "Authentication", "Enter your credentials"
                           , new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, InitialUsername = "MLib" }
                           );
            if (result == null)
            {
                //User pressed cancel
            }
            else
            {
                MessageDialogResult messageResult = await dlg.Manager.ShowMessageAsync(this, "Authentication Information", String.Format("Username: {0}\nPassword: {1}", result.Username, result.Password));
            }
        }


        private async void ShowLoginDialogOnlyPassword(object sender, RoutedEventArgs e)
        {
            var dlg = ServiceContainer.Instance.GetService<IContentDialogService>();

            ILoginDialogData result = await dlg.Manager.ShowLoginAsync(
                 this, "Authentication", "Enter your password"
                , new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, ShouldHideUsername = true }
                );

            if (result == null)
            {
                //User pressed cancel
            }
            else
            {
                MessageDialogResult messageResult = await dlg.Manager.ShowMessageAsync(this, "Authentication Information", String.Format("Password: {0}", result.Password));
            }
        }
        #endregion LoginDialog

        private async void ShowProgressDialog(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                NegativeButtonText = "Close now",
                AnimateShow = false,
                AnimateHide = false
            };

            var dlg = ServiceContainer.Instance.GetService<IContentDialogService>();

            var controller = await dlg.Manager.ShowProgressAsync(this, "Please wait...", "We are baking some cupcakes!", settings: mySettings);
            controller.SetIndeterminate();

            await Delay(5000);

            controller.SetCancelable(true);

            double i = 0.0;
            while (i < 6.0)
            {
                double val = (i / 100.0) * 20.0;
                controller.SetProgress(val);
                controller.SetMessage("Baking cupcake: " + i + "...");

                if (controller.IsCanceled)
                    break; //canceled progressdialog auto closes.

                i += 1.0;

                await Delay(2000);
            }

            await controller.CloseAsync();

            if (controller.IsCanceled)
            {
                await dlg.Manager.ShowMessageAsync(this, "No cupcakes!", "You stopped baking!");
            }
            else
            {
                await dlg.Manager.ShowMessageAsync(this, "Cupcakes!", "Your cupcakes are finished! Enjoy!");
            }
        }

        #region CustomCloseDialogTest
        private async void ShowAwaitCustomDialog(object sender, RoutedEventArgs e)
        {
            var dlg = ServiceContainer.Instance.GetService<IContentDialogService>();

            EventHandler<DialogStateChangedEventArgs> dialogManagerOnDialogOpened = null;
            dialogManagerOnDialogOpened = (o, args) => {
                dlg.Manager.DialogOpened -= dialogManagerOnDialogOpened;
                Console.WriteLine("Custom Dialog opened!");
            };
            dlg.Manager.DialogOpened += dialogManagerOnDialogOpened;

            EventHandler<DialogStateChangedEventArgs> dialogManagerOnDialogClosed = null;
            dialogManagerOnDialogClosed = async (o, args) => {
                dlg.Manager.DialogClosed -= dialogManagerOnDialogClosed;
                Console.WriteLine("Custom Dialog closed!");
                await dlg.Manager.ShowMessageAsync(this, "Dialog gone", "The custom dialog has closed");
            };

            dlg.Manager.DialogClosed += dialogManagerOnDialogClosed;

            var dialog = (IBaseMetroDialog)this.Resources["CustomCloseDialogTest"];

            await dlg.Manager.ShowMetroDialogAsync(this, dialog);
            await dialog.WaitUntilUnloadedAsync();
        }

        private async void CloseCustomDialog(object sender, RoutedEventArgs e)
        {
            var dlg = ServiceContainer.Instance.GetService<IContentDialogService>();

            var dialog = (IBaseMetroDialog)this.Resources["CustomCloseDialogTest"];

            await dlg.Manager.HideMetroDialogAsync(this, dialog);
        }
        #endregion CustomCloseDialogTest

        #region Show OutsideDialogs
        private void ShowInputDialogOutside(object sender, RoutedEventArgs e)
        {
            var metro = ServiceContainer.Instance.GetService<IMetroWindowService>();
            var dlg = ServiceContainer.Instance.GetService<IContentDialogService>();

            var result = dlg.Manager.ShowModalInputExternal(
                this
              , "Hello!", "What is your name?"
              , metro.CreateExternalWindow()
              );

            if (result == null)  // user pressed cancel
                return;

            dlg.Manager.ShowModalMessageExternal(
                this
              , "Hello"
              , "Hello " + result + "!"
              , metro.CreateExternalWindow()
              );
        }

        private void ShowLoginDialogOutside(object sender, RoutedEventArgs e)
        {
            var metro = ServiceContainer.Instance.GetService<IMetroWindowService>();
            var dlg = ServiceContainer.Instance.GetService<IContentDialogService>();

            ILoginDialogData result = dlg.Manager.ShowModalLoginExternal(
                  this
                , "Authentication"
                , "Enter your credentials"
                , metro.CreateExternalWindow()
                , new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, InitialUsername = "MLib", EnablePasswordPreview = true });

            if (result == null)
            {
                //User pressed cancel
            }
            else
            {
                MessageDialogResult messageResult = dlg.Manager.ShowModalMessageExternal(
                this
              , "Authentication Information",
                String.Format("Username: {0}\nPassword: {1}"
                              , result.Username, result.Password)
              , metro.CreateExternalWindow());
            }
        }

        private void ShowMessageDialogOutside(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Hi",
                NegativeButtonText = "Go away!",
                FirstAuxiliaryButtonText = "Cancel",
                ColorScheme = MetroDialogOptions.ColorScheme
            };

            var metro = ServiceContainer.Instance.GetService<IMetroWindowService>();
            var dlg = ServiceContainer.Instance.GetService<IContentDialogService>();

            MessageDialogResult result = dlg.Manager.ShowModalMessageExternal(
                this
               ,"Hello!"
              , "Welcome to the world of metro!"
              , metro.CreateExternalWindow()
              , MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary
              , mySettings);

            if (result != MessageDialogResult.FirstAuxiliary)
            {
                dlg.Manager.ShowModalMessageExternal(
                     this
                   , "Result"
                   , "You said: " + (result == MessageDialogResult.Affirmative ? mySettings.AffirmativeButtonText : mySettings.NegativeButtonText +
                      Environment.NewLine + Environment.NewLine + "This dialog will follow the Use Accent setting.")
                   , metro.CreateExternalWindow()
                    );
            }
        }
        #endregion Show OutsideDialogs
        private Task Delay(int dueTime)
        {
            return Task.Delay(dueTime);
        }
        #endregion Overlay Demo
    }
}
