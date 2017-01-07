namespace Settings
{
    using Settings.Interfaces;

    /// <summary>
    /// This class keeps track of program options and user profile (session) data.
    /// Both data items can be added and are loaded on application start to restore
    /// the program state of the last user session or to implement the default
    /// application state when starting the application for the very first time.
    /// </summary>
    public class SettingsManager
    {
        #region properties
        /// <summary>
        /// Implement <seealso cref="IOptionsPanel"/> method to query options from model container.
        /// </summary>
        public static ISettingsManager Instance
        {
            get
            {
                return new Manager.SettingsManagerImpl();
            }
        }
        #endregion properties
    }
}
