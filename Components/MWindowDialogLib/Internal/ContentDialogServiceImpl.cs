namespace MWindowDialogLib.Internal
{
    using MsgBox.Internal;
    using MWindowInterfacesLib.Interfaces;
    using MWindowInterfacesLib.MsgBox;

    /// <summary>
    /// Implements a service that shows content dialogs
    /// (MessageBoxes, LoginDialog etc) within the context
    /// of a given WPF Window.
    /// </summary>
    internal class ContentDialogServiceImpl : MWindowDialogLib.IContentDialogService
    {
        #region fields
        private readonly IDialogCoordinator _dialogCoordinator = null;
        private readonly IDialogManager _dialogManager = null;
        private readonly IMetroWindowService _metroWindowService;

        private IMessageBoxService _MsgBox = null;
        #endregion fields

        #region constructor
        /// <summary>
        /// Class constructor
        /// </summary>
        public ContentDialogServiceImpl()
        {
            _dialogManager = new DialogManager();
            _dialogCoordinator = new DialogCoordinator(_dialogManager);
        }

        public ContentDialogServiceImpl(IMetroWindowService metroWindowService)
            : this()
        {
            this._metroWindowService = metroWindowService;
        }
        #endregion constructor

        #region properties
        /// <summary>
        /// Gets an instance that implments the <seealso cref="IDialogManager"/> interface.
        /// </summary>
        public IDialogManager Manager
        {
            get
            {
                return _dialogManager;
            }
        }

        /// <summary>
        /// Gets an instance that implements the <seealso cref="IDialogCoordinator"/> interface.
        /// </summary>
        public IDialogCoordinator Coordinator
        {
            get
            {
                return _dialogCoordinator;
            }
        }

        /// <summary>
        /// Gets a message box service that can display message boxes
        /// in a variety of different configurations.
        /// </summary>
        public IMessageBoxService MsgBox
        {
            get
            {
                if (_MsgBox == null)
                    _MsgBox = new MessageBoxServiceImpl(this);

                return _MsgBox;
            }
        }
        #endregion properties
    }
}
