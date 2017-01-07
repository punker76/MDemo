namespace MWindowInterfacesLib.MsgBox
{
    using Enums;
    using System;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Defines an interface to a message box service that can
    /// display message boxes in a variety of different configurations.
    /// </summary>
    public interface IMessageBoxService
    {
        #region IMsgBoxService methods
        #region Simple Messages
        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// Only the <paramref name="messageBoxText"/> is a required parameter
        /// all others are up to the caller.
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<MsgBoxResult> Show(
              string messageBoxText
            , MsgBoxResult btnDefault = MsgBoxResult.None
            , object helpLink = null
            , string helpLinkTitle = ""
            , string helpLabel = ""
            , Func<object, bool> navigateHelplinkMethod = null
            , bool showCopyMessage = false
            );

        Task<MsgBoxResult> Show(
            string messageBoxText
          , string caption
          , MsgBoxResult btnDefault = MsgBoxResult.None
          , object helpLink = null
          , string helpLinkTitle = ""
          , string helpLabel = ""
          , Func<object, bool> navigateHelplinkMethod = null
          , bool showCopyMessage = false);

        Task<MsgBoxResult> Show(string messageBoxText, string caption,
          MsgBoxButtons buttonOption,
          MsgBoxResult btnDefault = MsgBoxResult.None,
          object helpLink = null,
          string helpLinkTitle = "",
          string helpLabel = "",
          Func<object, bool> navigateHelplinkMethod = null,
          bool showCopyMessage = false);

        Task<MsgBoxResult> Show(string messageBoxText, string caption,
                          MsgBoxButtons buttonOption, MsgBoxImage image,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false);

        Task<MsgBoxResult> Show(string messageBoxText, string caption,
                  string details,
                  MsgBoxButtons buttonOption, MsgBoxImage image,
                  MsgBoxResult btnDefault = MsgBoxResult.None,
                  object helpLink = null,
                  string helpLinkTitle = "",
                  string helpLabel = "",
                  Func<object, bool> navigateHelplinkMethod = null,
                  bool showCopyMessage = false);
        #endregion Simple Messages

        #region Messages with display of Exception
        Task<MsgBoxResult> Show(Exception exp, string caption,
                      MsgBoxButtons buttonOption, MsgBoxImage image,
                      MsgBoxResult btnDefault = MsgBoxResult.None,
                      object helpLink = null,
                      string helpLinkTitle = "",
                      string helpLabel = "",
                      Func<object, bool> navigateHelplinkMethod = null,
                      bool showCopyMessage = false);

        Task<MsgBoxResult> Show(Exception exp,
                  string textMessage = "", string caption = "",
                  MsgBoxButtons buttonOption = MsgBoxButtons.OK,
                  MsgBoxImage image = MsgBoxImage.Error,
                  MsgBoxResult btnDefault = MsgBoxResult.None,
                  object helpLink = null,
                  string helpLinkTitle = "",
                  string helpLabel = "",
                  Func<object, bool> navigateHelplinkMethod = null,
                  bool showCopyMessage = false);
        #endregion Messages with display of Exception

        #region Messages with explicit Context or Window Owner Paremeter
        Task<MsgBoxResult> Show(object ownerContext,
                          string messageBoxText, string caption = "",
                          MsgBoxButtons buttonOption = MsgBoxButtons.OK,
                          MsgBoxImage image = MsgBoxImage.Error,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLinkLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false);

        Task<MsgBoxResult> Show(object ownerContext,
                          string messageBoxText, string caption,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxButtons buttonOption = MsgBoxButtons.OK,
                          MsgBoxImage image = MsgBoxImage.Error,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLinkLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false);
        #endregion Messages with explicit Context or Window Owner Paremeter

        #region Explicit defaultCloseResult, dialogCanCloseViaChrome Paremeter (XXX TODO)
        Task<MsgBoxResult> Show(string messageBoxText,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false);

        Task<MsgBoxResult> Show(string messageBoxText, string caption,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false);

        Task<MsgBoxResult> Show(string messageBoxText, string caption,
                          MsgBoxButtons buttonOption,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false);

        Task<MsgBoxResult> Show(string messageBoxText, string caption,
                          MsgBoxButtons buttonOption, MsgBoxImage image,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false);

        Task<MsgBoxResult> Show(string messageBoxText, string caption,
                          string details,
                          MsgBoxButtons buttonOption, MsgBoxImage image,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false);
        #endregion Explicit defaultCloseResult, dialogCanCloseViaChrome Paremeter (XXX TODO)
        #endregion IMsgBoxService methods

        /// <summary>
        /// Attempts to find a suitable owner window by searching in
        /// 1) The registered context object associations <seealso cref="ContextRegistration"/> and
        /// 2) The standard collection of .Net window objects.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dialog"></param>
        /// <returns>The suitable or null if none was found.</returns>
        Window FindOwnerWindow(
            object context
          , Window dialog = null);
    }
}
