namespace MWindowDialogLib.Internal
{
    using Dialogs;
    using MWindowInterfaceLib.Interfaces.LoginDialog;
    using MWindowInterfacesLib.Enums;
    using MWindowInterfacesLib.Interfaces;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using Util;  //Extensions

    /// <summary>
    /// This class supports coordination of content dialogs from within
    /// a viewmodel that is attached to a window view.
    /// 
    /// The relevant methods contain a parameter called context to support
    /// this use case. The context is either:
    /// 
    /// 1) An implementation of <seealso cref="IMetroWindow"/> or
    /// 
    /// 2) A ViewModel that is bound to an <seealso cref="IMetroWindow"/> implementation
    ///    and registered via <seealso cref="DialogParticipation"/>.
    /// </summary>
    internal class DialogCoordinator : IDialogCoordinator
    {
        #region fields
        private readonly IDialogManager _dialogManager = null;
        #endregion fields

        public DialogCoordinator(IDialogManager dialogManager)
            : this()
        {
            _dialogManager = dialogManager;
        }

        protected DialogCoordinator()
        {

        }

        public Task<TDialog> GetCurrentDialogAsync<TDialog>(object context) where TDialog : IBaseMetroDialog
        {
            var metroWindow = GetMetroWindow(context);
            return metroWindow.Dispatcher.Invoke(() => metroWindow.GetCurrentDialogAsync<TDialog>());
        }

        public Task<MessageDialogResult> ShowMessageAsync(
            object context
          , string title
          , string message
          , MessageDialogStyle style = MessageDialogStyle.Affirmative
          , IMetroDialogSettings settings = null)
        {
            var metroWindow = GetMetroWindow(context);
            return metroWindow.Dispatcher.Invoke(() => _dialogManager.ShowMessageAsync(metroWindow, title, message, style, settings));
        }

        public Task<IProgressDialogController> ShowProgressAsync(
              object context
            , string title
            , string message
            , bool isCancelable = false
            , IMetroDialogSettings settings = null)
        {
            var metroWindow = GetMetroWindow(context);
            return metroWindow.Dispatcher.Invoke(() => _dialogManager.ShowProgressAsync(metroWindow, title, message, isCancelable, settings));
        }

        public Task ShowMetroDialogAsync(object context, IBaseMetroDialog dialog, IMetroDialogSettings settings = null)
        {
            var metroWindow = GetMetroWindow(context);
            return metroWindow.Dispatcher.Invoke(() => _dialogManager.ShowMetroDialogAsync(metroWindow, dialog, settings));
        }

        public Task HideMetroDialogAsync(object context, IBaseMetroDialog dialog, IMetroDialogSettings settings = null)
        {
            var metroWindow = GetMetroWindow(context);
            return metroWindow.Dispatcher.Invoke(() => _dialogManager.HideMetroDialogAsync(metroWindow, dialog, settings));
        }

        /// <summary>
        /// Shows the input dialog.
        /// </summary>
        /// <param name="context">Typically this should be the view model, which you register in XAML using <see cref="DialogParticipation.SetRegister"/>.</param>
        /// <param name="title">The title of the MessageDialog.</param>
        /// <param name="message">The message contained within the MessageDialog.</param>
        /// <param name="settings">Optional settings that override the global metro dialog settings.</param>
        /// <returns>The text that was entered or null (Nothing in Visual Basic) if the user cancelled the operation.</returns>
        public Task<string> ShowInputAsync(
              object context
            , string title
            , string message
            , IMetroDialogSettings settings = null)
        {
            var metroWindow = GetMetroWindow(context);

            return metroWindow.Dispatcher.Invoke(() => _dialogManager.ShowInputAsync(metroWindow, title, message, settings));
        }

        /// <summary>
        /// Creates a LoginDialog inside of the current window.
        /// </summary>
        /// <param name="context">Typically this should be the view model, which you register in XAML using <see cref="DialogParticipation.SetRegister"/>.</param>
        /// <param name="title">The title of the LoginDialog.</param>
        /// <param name="message">The message contained within the LoginDialog.</param>
        /// <param name="settings">Optional settings that override the global metro dialog settings.</param>
        /// <returns>The text that was entered or null (Nothing in Visual Basic) if the user cancelled the operation.</returns>
        public Task<ILoginDialogData> ShowLoginAsync(
              object context
            , string title
            , string message
            , ILoginDialogSettings settings = null)
        {
            var metroWindow = GetMetroWindow(context);
            return metroWindow.Dispatcher.Invoke(() => _dialogManager.ShowLoginAsync(metroWindow, title, message, settings));
        }

        /// <summary>
        /// Attempts to find the MetroWindow that should show the ContentDialog
        /// by searching the context object in the <seealso cref="DialogParticipation"/>
        /// object.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private IMetroWindow GetMetroWindow(object context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (!ContextRegistration.Instance.IsRegistered(context))
                throw new InvalidOperationException("Context is not registered. Consider using static class DialogParticipation.Register in XAML to bind in the DataContext.");

            var association = ContextRegistration.Instance.GetAssociation(context);
            var metroWindow = association.Invoke(() => Window.GetWindow(association) as IMetroWindow);

            if (metroWindow == null)
                throw new InvalidOperationException("Context is not inside a MetroWindow.");

            return metroWindow;
        }
    }
}
