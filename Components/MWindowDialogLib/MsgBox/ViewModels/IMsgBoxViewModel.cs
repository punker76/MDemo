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
        bool DialogCanCloseViaChrome { get; }
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