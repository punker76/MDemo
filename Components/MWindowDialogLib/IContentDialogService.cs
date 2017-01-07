namespace MWindowDialogLib
{
    using MWindowInterfacesLib.Interfaces;
    using MWindowInterfacesLib.MsgBox;

    /// <summary>
    /// This service is the root item for all other content dialog
    /// related services in this assembly.
    /// </summary>
    public interface IContentDialogService
    {
        #region properties
        /// <summary>
        /// Gets an instance of the <seealso cref="IDialogManager"/> object.
        /// </summary>
        IDialogManager Manager { get; }

        /// <summary>
        /// Gets an instance of the <seealso cref="IDialogCoordinator"/> object.
        /// </summary>
        IDialogCoordinator Coordinator { get; }

        /// <summary>
        /// Gets a message box service that can display message boxes
        /// in a variety of different configurations.
        /// </summary>
        IMessageBoxService MsgBox { get; }
        #endregion properties
    }
}
