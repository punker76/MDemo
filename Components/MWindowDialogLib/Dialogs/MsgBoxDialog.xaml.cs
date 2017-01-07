namespace MWindowDialogLib.Dialogs
{
    using MsgBox.ViewModels;
    using MWindowInterfacesLib.Interfaces;
    using MWindowInterfacesLib.MsgBox.Enums;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// An internal control that represents a message dialog. Please use MetroWindow.ShowMessage instead!
    /// </summary>
    public partial class MsgBoxDialog : BaseMetroDialog, IMsgBoxDialog
    {
        #region constructors
        public MsgBoxDialog()
            : base()
        {
        }

        internal MsgBoxDialog(IMetroWindow parentWindow)
            : this(parentWindow, null)
        {
        }

        internal MsgBoxDialog(IMetroWindow parentWindow, IMetroDialogSettings settings)
            : base(parentWindow, settings)
        {
            InitializeComponent();

            PART_MessageScrollViewer.Height = DialogSettings.MaximumBodyHeight;
        }
        #endregion constructors

        #region nethods
        public Task<MsgBoxResult> WaitForButtonPressAsync()
        {
            Dispatcher.BeginInvoke(new Action(() => {
                this.Focus();
            }));

            TaskCompletionSource<MsgBoxResult> tcs = new TaskCompletionSource<MsgBoxResult>();

            // List events that will be handled to exit the dialog with an enumerated result
            KeyEventHandler escapeKeyHandler = null;
            EventHandler dialgCloseResult = null;

            // This action should be invoked upon exiting a dialog
            Action cleanUpHandlers = null;

            var cancellationTokenRegistration = DialogSettings.CancellationToken.Register(() =>
            {
                cleanUpHandlers?.Invoke();
                tcs.TrySetResult(MsgBoxResult.Cancel);
            });

            // This action cleans-up all handlers added below and
            // should be invoked upon exiting the dialog
            cleanUpHandlers = () => {

                KeyDown -= escapeKeyHandler;
                DialogCloseResultEvent -= dialgCloseResult;

                cancellationTokenRegistration.Dispose();
            };

            // Handle keyboard events such as user presses enter or escape
            escapeKeyHandler = (sender, e) => {
                if (e.Key == Key.Escape)
                {
                    cleanUpHandlers();

                    // Escape is same indication as Cancel
                    tcs.TrySetResult(MsgBoxResult.Cancel);
                }
                else if (e.Key == Key.Enter)
                {
                    cleanUpHandlers();

                    // Enter key is same like clicking a button that has focus
                    // at the time (if there was any)
                    tcs.TrySetResult(GetResult());
                }
            };

            // Handle messagebox keyboard event (user clicked button in message box)
            dialgCloseResult = (sender, e) => {
                cleanUpHandlers();

                tcs.TrySetResult(GetResult());
            };

            // Add this event handlers to exit dialog when one of these events occurs
            KeyDown += escapeKeyHandler;
            DialogCloseResultEvent += dialgCloseResult;

            return tcs.Task;
        }

        protected override void OnLoaded()
        {
            ////            SetButtonState(this);
        }

        /// <summary>
        /// Try tp find the <seealso cref="MsgBoxResult"/> in the attached
        /// viewmodel if there is any of the expected type.
        /// </summary>
        /// <returns></returns>
        private MsgBoxResult GetResult()
        {
            var viewmodel = DataContext as IMsgBoxViewModel;

            if (viewmodel != null)
                return viewmodel.Result;

            return MsgBoxResult.None;
        }
        #endregion nethods
    }
}
