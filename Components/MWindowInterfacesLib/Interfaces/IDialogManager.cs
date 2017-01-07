namespace MWindowInterfacesLib.Interfaces
{
    using System.Threading.Tasks;
    using Enums;
    using MWindowInterfaceLib.Interfaces.LoginDialog;
    using Events;
    using System;
    using System.Windows;
    using MsgBox.Enums;

    /// <summary>
    /// Delegates a function or method that can be defined at run time.
    /// A method of this type creates a <seealso cref="IMetroWindow"/> and
    /// returns it uppon invocation.
    /// </summary>
    /// <returns></returns>
    public delegate IMetroWindow DelegateCreateExternalWindow();

    public interface IDialogManager
    {
        #region events
        event EventHandler<DialogStateChangedEventArgs> DialogOpened;
        event EventHandler<DialogStateChangedEventArgs> DialogClosed;
        #endregion events

        #region methods
////        Task<TDialog> GetCurrentDialogAsync<TDialog>(IMetroWindow metroWindow) where TDialog : IBaseMetroDialog;

        Task HideMetroDialogAsync(IMetroWindow metroWindow, IBaseMetroDialog dialog, IMetroDialogSettings settings = null);

        /// <summary>
        /// Creates a MsgBoxDialog inside of the current window.
        /// </summary>
        /// <param name="window">The MetroWindow</param>
        /// <param name="title">The title of the MessageDialog.</param>
        /// <param name="message">The message contained within the MessageDialog.</param>
        /// <param name="style">The type of buttons to use.</param>
        /// <param name="settings">Optional settings that override the global metro dialog settings.</param>
        /// <returns>A task promising the result of which button was pressed.</returns>
        Task<MsgBoxResult> ShowMsgBoxAsync(
              IMetroWindow metroWindow
            , IMsgBoxDialog dialog
            , MessageDialogStyle style = MessageDialogStyle.Affirmative
            , IMetroDialogSettings settings = null);

        Task<MessageDialogResult> ShowMessageAsync(IMetroWindow metroWindow, string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative, IMetroDialogSettings settings = null);

        Task ShowMetroDialogAsync(IMetroWindow metroWindow, IBaseMetroDialog dialog, IMetroDialogSettings settings = null);

        Task<IProgressDialogController> ShowProgressAsync(IMetroWindow metroWindow, string title, string message, bool isCancelable = false, IMetroDialogSettings settings = null);

        /// <summary>
        /// Creates a InputDialog inside of the current window.
        /// </summary>
        /// <param name="window">The MetroWindow</param>
        /// <param name="title">The title of the MessageDialog.</param>
        /// <param name="message">The message contained within the MessageDialog.</param>
        /// <param name="settings">Optional settings that override the global metro dialog settings.</param>
        /// <returns>The text that was entered or null (Nothing in Visual Basic) if the user cancelled the operation.</returns>
        Task<string> ShowInputAsync(
              IMetroWindow metroWindow
            , string title
            , string message
            , IMetroDialogSettings settings = null);

        /// <summary>
        /// Creates a LoginDialog inside of the current window.
        /// </summary>
        /// <param name="window">The window that is the parent of the dialog.</param>
        /// <param name="title">The title of the LoginDialog.</param>
        /// <param name="message">The message contained within the LoginDialog.</param>
        /// <param name="settings">Optional settings that override the global metro dialog settings.</param>
        /// <returns>The text that was entered or null (Nothing in Visual Basic) if the user cancelled the operation.</returns>
        Task<ILoginDialogData> ShowLoginAsync(
              IMetroWindow metroWindow
            , string title
            , string message
            , ILoginDialogSettings settings = null);

        #region Show External Modal Dialog Window
        /// <summary>
        /// Creates a InputDialog outside of the current window.
        /// </summary>
        /// <param name="metroWindow">The MetroWindow</param>
        /// <param name="title">The title of the MessageDialog.</param>
        /// <param name="message">The message contained within the MessageDialog.</param>
        /// <param name="dialogWindow">The outside modal window to be owned by a given <seealso cref="MetroWindow"/></param>
        /// <param name="settings">Optional settings that override the global metro dialog settings.</param>
        /// <returns>The text that was entered or null (Nothing in Visual Basic) if the user cancelled the operation.</returns>
        string ShowModalInputExternal(
              IMetroWindow metroWindow
            , string title
            , string message
            , Window dialogWindow
            , IMetroDialogSettings settings = null);


        /// <summary>
        /// Creates a MessageDialog ouside of the current window.
        /// </summary>
        /// <param name="metroWindow">The MetroWindow</param>
        /// <param name="title">The title of the MessageDialog.</param>
        /// <param name="message">The message contained within the MessageDialog.</param>
        /// <param name="dialogWindow">The outside modal window to be owned by a given <seealso cref="MetroWindow"/></param>
        /// <param name="style">The type of buttons to use.</param>
        /// <param name="settings">Optional settings that override the global metro dialog settings.</param>
        /// <returns>A task promising the result of which button was pressed.</returns>
        MessageDialogResult ShowModalMessageExternal(
              IMetroWindow metroWindow
            , string title
            , string message
            , Window dialogWindow
            , MessageDialogStyle style = MessageDialogStyle.Affirmative
            , IMetroDialogSettings settings = null);

        /// <summary>
        /// Creates a LoginDialog outside of the current window.
        /// </summary>
        /// <param name="metroWindow">The window that is the parent of the dialog.</param>
        /// <param name="title">The title of the LoginDialog.</param>
        /// <param name="message">The message contained within the LoginDialog.</param>
        /// <param name="dialogWindow"></param>
        /// <param name="settings">Optional settings that override the global metro dialog settings.</param>
        /// <returns>The text that was entered or null (Nothing in Visual Basic) if the user cancelled the operation.</returns>
        ILoginDialogData ShowModalLoginExternal(
              IMetroWindow metroWindow
            , string title
            , string message
            , Window dialogWindow
            , ILoginDialogSettings settings = null);
        #endregion Show External Modal Dialog Window
        #endregion methods
    }
}