namespace MWindowDialogLib.MsgBox.ViewModels
{
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Windows.Media;
    using MWindowInterfacesLib.MsgBox.Enums;

    internal interface IMsgBoxViewModel
    {
        string AllToString { get; }
        ICommand CancelCommand { get; }
        bool CancelVisibility { get; set; }
        ICommand CloseCommand { get; }
        bool CloseVisibility { get; set; }
        ImageSource CopyImageSource { get; set; }
        ICommand CopyText { get; }


        MsgBoxResult DefaultCloseResult { get; }

        /// <summary>
        /// Use this property to determine whether the dialog can be closed
        /// without picking a choice (e.g. OK or Cancel) or not.
        /// </summary>
        bool DialogCanCloseViaChrome { get; }

        /// <summary>
        /// Use this property to tell the view that the viewmodel would like to close now.
        /// </summary>
        bool? DialogCloseResult { get; }
        bool DisplayHelpLink { get; }
        bool EnableCopyFunction { get; set; }
        string HelpLink { get; set; }
        string HelpLinkTitle { get; set; }
        string HyperlinkLabel { get; set; }
        string InnerMessageDetails { get; set; }
        MsgBoxResult IsDefaultButton { get; }
        string Message { get; set; }
        ICommand NavigateToUri { get; }
        ICommand NoCommand { get; }
        ICommand OkCommand { get; }
        bool OkVisibility { get; set; }
        MsgBoxResult Result { get; }
        bool ShowDetails { get; set; }
        string Title { get; set; }
        MsgBoxImage TypeOfImage { get; }
        ICommand YesCommand { get; }
        bool YesNoVisibility { get; set; }

        void MessageBox_Closing(object sender, CancelEventArgs e);
    }
}