namespace MLib
{
    /// <summary>
    /// The AppearanceManager class manages all WPF theme related items.
    /// Its location in the hierarchy is between the viewmodels and the
    /// themes settings service.
    /// </summary>
    public class AppearanceManager
    {
        #region properties
        /// <summary>
        /// Gets an instance of the Appearance Manager component.
        /// This component manages theme related things, such as:
        /// theme selection Dark/Light etc..
        /// </summary>
        public static IAppearanceManager Instance
        {
            get
            {
                return new Internal.AppearanceManagerImp();
            }
        }
        #endregion properties
    }
}
