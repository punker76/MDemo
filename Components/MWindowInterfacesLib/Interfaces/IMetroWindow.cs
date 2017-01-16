namespace MWindowInterfacesLib.Interfaces
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    public interface IMetroWindow
    {
        #region properties
        //
        // Summary:
        //     Gets the rendered height of this element.
        //
        // Returns:
        //     The element's height, as a value in device-independent units (1/96th inch per
        //     unit). The default value is 0 (zero).
        double ActualHeight { get; }

        //
        // Summary:
        //     Gets the rendered width of this element.
        //
        // Returns:
        //     The element's width, as a value in device-independent units (1/96th inch per
        //     unit). The default value is 0 (zero).
        double ActualWidth { get; }

        //
        // Summary:
        //     Gets the System.Windows.Threading.Dispatcher this System.Windows.Threading.DispatcherObject
        //     is associated with.
        //
        // Returns:
        //     The dispatcher.
        [EditorBrowsable(EditorBrowsableState.Advanced)]

        Dispatcher Dispatcher { get; }

        //
        // Summary:
        //     Gets or sets the System.Windows.Window that owns this System.Windows.Window.
        //
        // Returns:
        //     A System.Windows.Window object that represents the owner of this System.Windows.Window.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     A window tries to own itself-or-Two windows try to own each other.
        //
        //   T:System.InvalidOperationException:
        //     The System.Windows.Window.Owner property is set on a visible window shown using
        //     System.Windows.Window.ShowDialog-or-The System.Windows.Window.Owner property
        //     is set with a window that has not been previously shown.
        [DefaultValue(null)]
        Window Owner { get; set; }

        IMetroDialogSettings MetroDialogOptions { get; set; }

        Grid OverlayBox { get; }
        Grid MetroActiveDialogContainer { get; }
        Grid MetroInactiveDialogContainer { get; }

        /// <summary>
        /// Determines if there is currently a ContentDialog visible or not.
        /// </summary>
        bool IsContentDialogVisible { get; }
        #endregion properties

        #region events
        //
        // Summary:
        //     Occurs when either the System.Windows.FrameworkElement.ActualHeight or the System.Windows.FrameworkElement.ActualWidth
        //     properties change value on this element.
        event SizeChangedEventHandler SizeChanged;
        #endregion events

        #region methods
        void ShowOverlay();

        /// <summary>
        /// Begins to show the MetroWindow's overlay effect.
        /// </summary>
        /// <returns>A task representing the process.</returns>
        System.Threading.Tasks.Task ShowOverlayAsync();

        void HideOverlay();

        /// <summary>
        /// Begins to hide the MetroWindow's overlay effect.
        /// </summary>
        /// <returns>A task representing the process.</returns>
        System.Threading.Tasks.Task HideOverlayAsync();

        /// <summary>
        /// Stores the given element, or the last focused element via FocusManager, for restoring the focus after closing a dialog.
        /// </summary>
        /// <param name="thisElement">The element which will be focused again.</param>
        void StoreFocus(IInputElement thisElement = null);

        void RestoreFocus();

        #region DialogManager
        /// <summary>
        /// Creates a MessageDialog inside of the current window.
        /// </summary>
        /// <param name="window">The MetroWindow</param>
        /// <param name="title">The title of the MessageDialog.</param>
        /// <param name="message">The message contained within the MessageDialog.</param>
        /// <param name="style">The type of buttons to use.</param>
        /// <param name="settings">Optional settings that override the global metro dialog settings.</param>
        /// <returns>A task promising the result of which button was pressed.</returns>
////        Task<MessageDialogResult> ShowMessageAsync(
////              string title
////            , string message
////            , MessageDialogStyle style = MessageDialogStyle.Affirmative
////            , IMetroDialogSettings settings = null);

        /// <summary>
        /// Creates a ProgressDialog inside of the current window.
        /// </summary>
        /// <param name="window">The MetroWindow</param>
        /// <param name="title">The title of the ProgressDialog.</param>
        /// <param name="message">The message within the ProgressDialog.</param>
        /// <param name="isCancelable">Determines if the cancel button is visible.</param>
        /// <param name="settings">Optional Settings that override the global metro dialog settings.</param>
        /// <returns>A task promising the instance of ProgressDialogController for this operation.</returns>
////        Task<IProgressDialogController> ShowProgressAsync(string title
////            , string message
////            , bool isCancelable = false
////            , IMetroDialogSettings settings = null);

        /// <summary>
        /// Hides a visible Metro Dialog instance.
        /// </summary>
        /// <param name="window">The window with the dialog that is visible.</param>
        /// <param name="dialog">The dialog instance to hide.</param>
        /// <param name="settings">An optional pre-defined settings instance.</param>
        /// <returns>A task representing the operation.</returns>
        /// <exception cref="InvalidOperationException">
        /// The <paramref name="dialog"/> is not visible in the window.
        /// This happens if <see cref="ShowMetroDialogAsync"/> hasn't been called before.
        /// </exception>
////        Task HideMetroDialogAsync(
////              IBaseMetroDialog dialog
////            , IMetroDialogSettings settings = null);

        /// <summary>
        /// Gets the current shown dialog in async way.
        /// </summary>
        /// <param name="window">The dialog owner.</param>
        Task<TDialog> GetCurrentDialogAsync<TDialog>() where TDialog : IBaseMetroDialog;

        /// <summary>
        /// Adds a Metro Dialog instance to the specified window and makes it visible asynchronously.
        /// If you want to wait until the user has closed the dialog, use <see cref="ShowMetroDialogAsyncAwaitable"/>
        /// <para>You have to close the resulting dialog yourself with <see cref="HideMetroDialogAsync"/>.</para>
        /// </summary>
        /// <param name="window">The owning window of the dialog.</param>
        /// <param name="dialog">The dialog instance itself.</param>
        /// <param name="settings">An optional pre-defined settings instance.</param>
        /// <returns>A task representing the operation.</returns>
        /// <exception cref="InvalidOperationException">The <paramref name="dialog"/> is already visible in the window.</exception>
////        Task ShowMetroDialogAsync(IBaseMetroDialog dialog,
////            IMetroDialogSettings settings = null);
        #endregion 
        #endregion methods
    }
}
