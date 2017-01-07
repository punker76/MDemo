namespace MWindowLib
{
    using MWindowInterfacesLib.Interfaces;

    /// <summary>
    /// Implements a static property that instantiates a service component.
    /// </summary>
    public class MetroWindowService
    {
        #region properties
        /// <summary>
        /// Gets an instance of the MetroWindowService service component.
        /// This service component creates Metro Window instances and supports
        /// utillity functions ...
        /// </summary>
        public static IMetroWindowService Instance
        {
            get
            {
                return new Internal.MetroWindowServiceImpl();
            }
        }
        #endregion properties
    }
}
