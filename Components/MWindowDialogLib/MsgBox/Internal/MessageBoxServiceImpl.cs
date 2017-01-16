namespace MWindowDialogLib.MsgBox.Internal
{
    using Dialogs;
    using MWindowDialogLib.Internal;
    using MWindowInterfacesLib.Interfaces;
    using MWindowInterfacesLib.MsgBox;
    using MWindowInterfacesLib.MsgBox.Enums;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;
    using ViewModels;

    internal class MessageBoxServiceImpl : IMessageBoxService
    {
        private IContentDialogService _DlgService;

        #region constructors
        public MessageBoxServiceImpl(IContentDialogService contentDialogServiceImpl)
        {
            _DlgService = contentDialogServiceImpl;
        }

        protected MessageBoxServiceImpl()
        {
            _DlgService = null;
        }
        #endregion constructors

        #region methods
        #region IMsgBoxService methods
        #region Simple Messages
        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// Only the <paramref name="messageBoxText"/> is a required parameter
        /// (see actual interface definition for defaults of optional parameters.
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
        public async Task<MsgBoxResult> ShowAsync(
              string messageBoxText
            , MsgBoxResult btnDefault = MsgBoxResult.None
            , object helpLink = null
            , string helpLinkTitle = ""
            , string helpLabel = ""
            , Func<object, bool> navigateHelplinkMethod = null
            , bool showCopyMessage = false
            )
        {
            var vm = this.InitializeViewModel(null, messageBoxText, string.Empty, string.Empty,
                                              MsgBoxButtons.OK, MsgBoxImage.Default, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// Only the <paramref name="messageBoxText"/> is a required parameter
        /// all others are optional.
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
        MsgBoxResult IMessageBoxService.Show(
              string messageBoxText
            , MsgBoxResult btnDefault
            , object helpLink
            , string helpLinkTitle
            , string helpLabel
            , Func<object, bool> navigateHelplinkMethod
            , bool showCopyMessage
            )
        {
            return WaitWithPumping(ShowAsync(messageBoxText
                                           , btnDefault, helpLink, helpLinkTitle, helpLabel
                                           , navigateHelplinkMethod, showCopyMessage));
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// Only the <paramref name="messageBoxText"/> and 
        /// <paramref name="caption"/> are a required parameters
        /// all others are optional.
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
        public async Task<MsgBoxResult> ShowAsync(
                    string messageBoxText
                  , string caption
                  , MsgBoxResult btnDefault = MsgBoxResult.None
                  , object helpLink = null
                  , string helpLinkTitle = ""
                  , string helpLabel = ""
                  , Func<object, bool> navigateHelplinkMethod = null
                  , bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(null, messageBoxText, caption, string.Empty,
                                              MsgBoxButtons.OK, MsgBoxImage.Default, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// Only the <paramref name="messageBoxText"/> and 
        /// <paramref name="caption"/> are a required parameters
        /// all others are optional.
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
        MsgBoxResult IMessageBoxService.Show(
            string messageBoxText
          , string caption
          , MsgBoxResult btnDefault
          , object helpLink
          , string helpLinkTitle
          , string helpLabel
          , Func<object, bool> navigateHelplinkMethod
          , bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(messageBoxText, caption
                                           , btnDefault, helpLink, helpLinkTitle, helpLabel
                                           , navigateHelplinkMethod, showCopyMessage));
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// The parameters:
        /// <paramref name="messageBoxText"/> and 
        /// <paramref name="caption"/>
        /// <paramref name="buttonOption"/>
        /// are a required parameters.
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
        public async Task<MsgBoxResult> ShowAsync(string messageBoxText, string caption,
                  MsgBoxButtons buttonOption,
              MsgBoxResult btnDefault = MsgBoxResult.None,
              object helpLink = null,
              string helpLinkTitle = "",
              string helpLabel = "",
              Func<object, bool> navigateHelplinkMethod = null,
              bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(null, messageBoxText, caption, string.Empty,
                                              buttonOption, MsgBoxImage.Default, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// The parameters:
        /// <paramref name="messageBoxText"/> and 
        /// <paramref name="caption"/>
        /// <paramref name="buttonOption"/>
        /// are a required parameters.
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
        MsgBoxResult IMessageBoxService.Show(string messageBoxText, string caption,
              MsgBoxButtons buttonOption,
              MsgBoxResult btnDefault,
              object helpLink,
              string helpLinkTitle,
              string helpLabel,
              Func<object, bool> navigateHelplinkMethod,
              bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(messageBoxText, caption, buttonOption
                                           , btnDefault, helpLink, helpLinkTitle, helpLabel
                                           , navigateHelplinkMethod, showCopyMessage));
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// The parameters:
        /// <paramref name="messageBoxText"/> and 
        /// <paramref name="caption"/>
        /// <paramref name="buttonOption"/>
        /// <param name="image"></param>
        /// are a required parameters.
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
        public async Task<MsgBoxResult> ShowAsync(string messageBoxText, string caption,
                  MsgBoxButtons buttonOption, MsgBoxImage image,
                  MsgBoxResult btnDefault,
                        object helpLink = null,
                        string helpLinkTitle = "",
                        string helpLabel = "",
                        Func<object, bool> navigateHelplinkMethod = null,
                        bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(null, messageBoxText, caption, string.Empty,
                                              buttonOption, image, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// The parameters:
        /// <paramref name="messageBoxText"/> and 
        /// <paramref name="caption"/>
        /// <paramref name="buttonOption"/>
        /// <param name="image"></param>
        /// are a required parameters.
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
        MsgBoxResult IMessageBoxService.Show(string messageBoxText, string caption,
                          MsgBoxButtons buttonOption, MsgBoxImage image,
                          MsgBoxResult btnDefault,
                          object helpLink,
                          string helpLinkTitle,
                          string helpLabel,
                          Func<object, bool> navigateHelplinkMethod,
                          bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(messageBoxText, caption, buttonOption, image
                                           , btnDefault, helpLink, helpLinkTitle, helpLabel
                                           , navigateHelplinkMethod, showCopyMessage));
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// The parameters:
        /// <paramref name="messageBoxText"/> and 
        /// <paramref name="caption"/>
        /// <paramref name="details"/>
        /// <paramref name="buttonOption"/>
        /// <param name="image"></param>
        /// are a required parameters.
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
        public async Task<MsgBoxResult> ShowAsync(string messageBoxText, string caption,
          string details,
          MsgBoxButtons buttonOption, MsgBoxImage image,
                  MsgBoxResult btnDefault = MsgBoxResult.None,
                  object helpLink = null,
                  string helpLinkTitle = "",
                  string helpLabel = "",
                  Func<object, bool> navigateHelplinkMethod = null,
                  bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(null, messageBoxText, caption, details,
                                              buttonOption, image, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// The parameters:
        /// <paramref name="messageBoxText"/> and 
        /// <paramref name="caption"/>
        /// <paramref name="details"/>
        /// <paramref name="buttonOption"/>
        /// <param name="image"></param>
        /// are a required parameters.
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
        MsgBoxResult IMessageBoxService.Show(string messageBoxText, string caption,
                  string details,
                  MsgBoxButtons buttonOption, MsgBoxImage image,
                 MsgBoxResult btnDefault,
                  object helpLink,
                  string helpLinkTitle,
                  string helpLabel,
                  Func<object, bool> navigateHelplinkMethod,
                  bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(messageBoxText, caption, details, buttonOption, image
                                           , btnDefault, helpLink, helpLinkTitle, helpLabel
                                           , navigateHelplinkMethod, showCopyMessage));
        }
        #endregion Simple Messages

        #region Messages with display of Exception
        /// <summary>
        /// Show a message box with a standard Exception display to the user.
        /// The parameters:
        /// <paramref name="exp"/>
        /// <paramref name="messageBoxText"/> and 
        /// <paramref name="caption"/>
        /// <paramref name="buttonOption"/>
        /// <param name="image"></param>
        /// are a required parameters.
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="caption"></param>
        /// <param name="buttonOption"></param>
        /// <param name="image"></param>
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        public async Task<MsgBoxResult> ShowAsync(Exception exp, string caption,
                      MsgBoxButtons buttonOption, MsgBoxImage image,
                      MsgBoxResult btnDefault = MsgBoxResult.None,
                      object helpLink = null,
                      string helpLinkTitle = "",
                      string helpLabel = "",
                      Func<object, bool> navigateHelplinkMethod = null,
                      bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(exp, string.Empty, caption, string.Empty,
                                              buttonOption, image, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }

        /// <summary>
        /// Show a message box with a standard Exception display to the user.
        /// The parameters:
        /// <paramref name="exp"/>
        /// <paramref name="messageBoxText"/> and 
        /// <paramref name="caption"/>
        /// <paramref name="buttonOption"/>
        /// <param name="image"></param>
        /// are a required parameters.
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="caption"></param>
        /// <param name="buttonOption"></param>
        /// <param name="image"></param>
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        MsgBoxResult IMessageBoxService.Show(Exception exp, string caption,
              MsgBoxButtons buttonOption, MsgBoxImage image,
              MsgBoxResult btnDefault,
              object helpLink,
              string helpLinkTitle,
              string helpLabel,
              Func<object, bool> navigateHelplinkMethod,
              bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(exp, caption, buttonOption, image,
              btnDefault, helpLink, helpLinkTitle,
              helpLabel, navigateHelplinkMethod, showCopyMessage));
        }

        /// <summary>
        /// Show a message box with a standard Exception display to the user.
        /// Only the <paramref name="exp"/> parameter is required all others are optional.
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="caption"></param>
        /// <param name="buttonOption"></param>
        /// <param name="image"></param>
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        public async Task<MsgBoxResult> ShowAsync(Exception exp,
                  string textMessage = "", string caption = "",
                  MsgBoxButtons buttonOption = MsgBoxButtons.OK,
                  MsgBoxImage image = MsgBoxImage.Error,
                  MsgBoxResult btnDefault = MsgBoxResult.None,
                  object helpLink = null,
                  string helpLinkTitle = "",
                  string helpLabel = "",
                  Func<object, bool> navigateHelplinkMethod = null,
                  bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(exp, textMessage, caption, string.Empty,
                                              buttonOption, image, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }


        /// <summary>
        /// Show a message box with a standard Exception display to the user.
        /// Only the <paramref name="exp"/> parameter is required all others are optional.
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="caption"></param>
        /// <param name="buttonOption"></param>
        /// <param name="image"></param>
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        MsgBoxResult IMessageBoxService.Show(Exception exp,
                  string textMessage, string caption,
                  MsgBoxButtons buttonOption,
                  MsgBoxImage image,
                  MsgBoxResult btnDefault,
                  object helpLink,
                  string helpLinkTitle,
                  string helpLabel,
                  Func<object, bool> navigateHelplinkMethod,
                  bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(exp,
                   textMessage, caption, buttonOption, image, btnDefault,
                   helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod, showCopyMessage));
        }
        #endregion Messages with display of Exception

        #region Explicit Context/Window Owner, defaultCloseResult, dialogCanCloseViaChrome Paremeter
        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// Only <param name="ownerContext"/> and <paramref name="messageBoxText"/>
        /// are required parameters
        /// (see actual interface definition for defaults of optional parameters.
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <param name="ownerContext"></param>
        /// <returns></returns>
        public async Task<MsgBoxResult> ShowAsync(object ownerContext,
                          string messageBoxText, string caption = "",
                          MsgBoxButtons buttonOption = MsgBoxButtons.OK,
                          MsgBoxImage image = MsgBoxImage.Error,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLinkLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false)

        {
            var vm = this.InitializeViewModel(null, messageBoxText, string.Empty, string.Empty,
                                              MsgBoxButtons.OK, MsgBoxImage.Default, btnDefault,
                                              helpLink, helpLinkTitle, helpLinkLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// Only <param name="ownerContext"/> and <paramref name="messageBoxText"/>
        /// are required parameters
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <param name="ownerContext"></param>
        /// <returns></returns>
        MsgBoxResult IMessageBoxService.Show(object ownerContext,
                          string messageBoxText, string caption,
                          MsgBoxButtons buttonOption,
                          MsgBoxImage image,
                          MsgBoxResult btnDefault,
                          object helpLink,
                          string helpLinkTitle,
                          string helpLinkLabel,
                          Func<object, bool> navigateHelplinkMethod,
                          bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(ownerContext,
                          messageBoxText, caption,
                          buttonOption, image, btnDefault,
                          helpLink, helpLinkTitle, helpLinkLabel,
                          navigateHelplinkMethod, showCopyMessage));
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// Only the
        /// <paramref name="ownerContext"/>,
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="ownerContext"/>,
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="caption"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        public async Task<MsgBoxResult> ShowAsync(object ownerContext,
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
                          bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(null, messageBoxText, caption, string.Empty,
                                              buttonOption, image, btnDefault,
                                              helpLink, helpLinkTitle, helpLinkLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, ownerContext);
        }

        /// <summary>
        /// Show a simple message (minimal with OK button) to the user.
        /// Only the
        /// <paramref name="ownerContext"/>,
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="caption"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        MsgBoxResult IMessageBoxService.Show(object ownerContext,
                          string messageBoxText, string caption,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxButtons buttonOption,
                          MsgBoxImage image,
                          MsgBoxResult btnDefault,
                          object helpLink,
                          string helpLinkTitle,
                          string helpLinkLabel,
                          Func<object, bool> navigateHelplinkMethod,
                          bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(ownerContext, messageBoxText, caption, defaultCloseResult, dialogCanCloseViaChrome,
                          buttonOption, image, btnDefault, helpLink, helpLinkTitle, helpLinkLabel,
                          navigateHelplinkMethod, showCopyMessage));
        }
        #endregion Explicit Context/Window Owner, defaultCloseResult, dialogCanCloseViaChrome Paremeter

        #region Explicit defaultCloseResult, dialogCanCloseViaChrome Paremeter (XXX TODO)
        /// <summary>
        /// Show a message dialog to the user.
        /// Only the
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="defaultCloseResult"></param>
        /// <param name="dialogCanCloseViaChrome"></param>
        /// 
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLinkLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        public async Task<MsgBoxResult> ShowAsync(string messageBoxText,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(null, messageBoxText, string.Empty, string.Empty,
                                              MsgBoxButtons.OK, MsgBoxImage.Information, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }

        /// <summary>
        /// Show a message dialog to the user.
        /// Only the
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="defaultCloseResult"></param>
        /// <param name="dialogCanCloseViaChrome"></param>
        /// 
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLinkLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        MsgBoxResult IMessageBoxService.Show(string messageBoxText,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault,
                          object helpLink,
                          string helpLinkTitle,
                          string helpLinkLabel,
                          Func<object, bool> navigateHelplinkMethod,
                          bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(messageBoxText, defaultCloseResult, dialogCanCloseViaChrome,
                          btnDefault,
                          helpLink, helpLinkTitle, helpLinkLabel,
                          navigateHelplinkMethod, showCopyMessage));
        }

        /// <summary>
        /// Show a message dialog to the user.
        /// Only the
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="caption"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="defaultCloseResult"></param>
        /// <param name="dialogCanCloseViaChrome"></param>
        /// 
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLinkLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        public async Task<MsgBoxResult> ShowAsync(string messageBoxText, string caption,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(null, messageBoxText, caption, string.Empty,
                                              MsgBoxButtons.OK, MsgBoxImage.Information, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }

        /// <summary>
        /// Show a message dialog to the user.
        /// Only the
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="caption"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="defaultCloseResult"></param>
        /// <param name="dialogCanCloseViaChrome"></param>
        /// 
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLinkLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        MsgBoxResult IMessageBoxService.Show(string messageBoxText, string caption,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault,
                          object helpLink,
                          string helpLinkTitle,
                          string helpLinkLabel,
                          Func<object, bool> navigateHelplinkMethod,
                          bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(messageBoxText, caption, defaultCloseResult, dialogCanCloseViaChrome,
                          btnDefault, helpLink, helpLinkTitle, helpLinkLabel,
                          navigateHelplinkMethod, showCopyMessage));
        }

        /// <summary>
        /// Show a message dialog to the user.
        /// Only the
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="caption"/>,
        /// <paramref name="buttonOption"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="buttonOption"></param>
        /// <param name="defaultCloseResult"></param>
        /// <param name="dialogCanCloseViaChrome"></param>
        /// 
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLinkLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        public async Task<MsgBoxResult> ShowAsync(string messageBoxText, string caption,
                          MsgBoxButtons buttonOption,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(null, messageBoxText, caption, string.Empty,
                                              buttonOption, MsgBoxImage.Information, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage, defaultCloseResult, dialogCanCloseViaChrome
                                              );

            return await Show(vm, null);
        }

        /// <summary>
        /// Show a message dialog to the user.
        /// Only the
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="caption"/>,
        /// <paramref name="buttonOption"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="buttonOption"></param>
        /// <param name="defaultCloseResult"></param>
        /// <param name="dialogCanCloseViaChrome"></param>
        /// 
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLinkLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        MsgBoxResult IMessageBoxService.Show(string messageBoxText, string caption,
                          MsgBoxButtons buttonOption,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault,
                          object helpLink,
                          string helpLinkTitle,
                          string helpLinkLabel,
                          Func<object, bool> navigateHelplinkMethod,
                          bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(messageBoxText, caption, buttonOption, defaultCloseResult, dialogCanCloseViaChrome,
                          btnDefault, helpLink, helpLinkTitle, helpLinkLabel,
                          navigateHelplinkMethod, showCopyMessage));
        }

        /// <summary>
        /// Show a message dialog to the user.
        /// Only the
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="caption"/>,
        /// <paramref name="buttonOption"/>,
        /// <paramref name="image"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="buttonOption"></param>
        /// <param name="image"></param>
        /// <param name="defaultCloseResult"></param>
        /// <param name="dialogCanCloseViaChrome"></param>
        /// 
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLinkLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        public async Task<MsgBoxResult> ShowAsync(string messageBoxText, string caption,
                          MsgBoxButtons buttonOption, MsgBoxImage image,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(null, messageBoxText, caption, string.Empty,
                                              buttonOption, image, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }

        /// <summary>
        /// Show a message dialog to the user.
        /// Only the
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="caption"/>,
        /// <paramref name="buttonOption"/>,
        /// <paramref name="image"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="buttonOption"></param>
        /// <param name="image"></param>
        /// <param name="defaultCloseResult"></param>
        /// <param name="dialogCanCloseViaChrome"></param>
        /// 
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        MsgBoxResult IMessageBoxService.Show(string messageBoxText, string caption,
                          MsgBoxButtons buttonOption, MsgBoxImage image,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault,
                          object helpLink,
                          string helpLinkTitle,
                          string helpLabel,
                          Func<object, bool> navigateHelplinkMethod,
                          bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(messageBoxText, caption, buttonOption, image, defaultCloseResult, dialogCanCloseViaChrome,
                          btnDefault,
                          helpLink, helpLinkTitle, helpLabel,
                          navigateHelplinkMethod, showCopyMessage));
        }

        /// <summary>
        /// Show a message dialog to the user.
        /// Only the
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="caption"/>,
        /// <paramref name="details"/>,
        /// <paramref name="buttonOption"/>,
        /// <paramref name="image"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="details"></param>
        /// <param name="buttonOption"></param>
        /// <param name="image"></param>
        /// <param name="defaultCloseResult"></param>
        /// <param name="dialogCanCloseViaChrome"></param>
        /// 
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLinkLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        public async Task<MsgBoxResult> ShowAsync(string messageBoxText, string caption,
                          string details,
                          MsgBoxButtons buttonOption, MsgBoxImage image,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault = MsgBoxResult.None,
                          object helpLink = null,
                          string helpLinkTitle = "",
                          string helpLabel = "",
                          Func<object, bool> navigateHelplinkMethod = null,
                          bool showCopyMessage = false)
        {
            var vm = this.InitializeViewModel(null, messageBoxText, caption, details,
                                              buttonOption, image, btnDefault,
                                              helpLink, helpLinkTitle, helpLabel, navigateHelplinkMethod,
                                              showCopyMessage
                                              );

            return await Show(vm, null);
        }


        /// <summary>
        /// Show a message dialog to the user.
        /// Only the
        /// <paramref name="messageBoxText"/>,
        /// <paramref name="caption"/>,
        /// <paramref name="details"/>,
        /// <paramref name="buttonOption"/>,
        /// <paramref name="image"/>,
        /// <paramref name="defaultCloseResult"/>,
        /// <paramref name="dialogCanCloseViaChrome"/> are required parameters
        /// XXX
        /// XXX TODO: dialogCanCloseViaChrome and defaultCloseResult are NOT supported yet
        /// XXX
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="details"></param>
        /// <param name="buttonOption"></param>
        /// <param name="image"></param>
        /// <param name="defaultCloseResult"></param>
        /// <param name="dialogCanCloseViaChrome"></param>
        /// 
        /// <param name="btnDefault"></param>
        /// <param name="helpLink"></param>
        /// <param name="helpLinkTitle"></param>
        /// <param name="helpLinkLabel"></param>
        /// <param name="navigateHelplinkMethod"></param>
        /// <param name="showCopyMessage"></param>
        /// <returns></returns>
        MsgBoxResult IMessageBoxService.Show(string messageBoxText, string caption,
                          string details,
                          MsgBoxButtons buttonOption, MsgBoxImage image,
                          MsgBoxResult defaultCloseResult,
                          bool dialogCanCloseViaChrome,
                          MsgBoxResult btnDefault,
                          object helpLink,
                          string helpLinkTitle,
                          string helpLinkLabel,
                          Func<object, bool> navigateHelplinkMethod,
                          bool showCopyMessage)
        {
            return WaitWithPumping(ShowAsync(messageBoxText, caption, details, buttonOption, image, defaultCloseResult, dialogCanCloseViaChrome,
                          btnDefault,
                          helpLink, helpLinkTitle, helpLinkLabel,
                          navigateHelplinkMethod, showCopyMessage));
        }
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
        public Window FindOwnerWindow(
                object context
              , Window dialog = null)
        {
            Window mainWindow = context as Window;
            Window dialogOwner = null;

            if (mainWindow == null)
            {
                // Lets see if this context is registered
                if (context != null)
                {
                    mainWindow = ContextRegistration.Instance.GetAssociation(context) as Window;

                    if (mainWindow != null)
                        dialogOwner = mainWindow;
                }

                // Context is not registered - lets try and find a suitable window anyway
                if (mainWindow == null)
                {
                    if (Application.Current != null)
                    {
                        if (dialog != null)
                        {
                            if (dialog != Application.Current.MainWindow)
                                dialogOwner = Application.Current.MainWindow;
                            else
                                dialogOwner = GetOwnerWindow();
                        }
                        else // dialog == null
                        {
                            if (Application.Current.MainWindow != null)
                                dialogOwner = Application.Current.MainWindow;
                            else
                                dialogOwner = GetOwnerWindow();
                        }
                    }
                }
            }
            else
            {
                if (dialog != null)
                {
                    if (dialog != mainWindow)
                        dialogOwner = mainWindow;
                }
            }

            if (dialog != null)
            {
                // Last chance check to make sure window can open without main window
                // (eg.: in start-up or after shut-down)
                if (dialogOwner == dialog)
                    dialogOwner = null;
            }

            return dialogOwner;
        }

        #region private methods
        /// <summary>
        /// Display a message box based on a given view model.
        /// </summary>
        /// <param name="viewModel">The viewmodel contains additional
        /// parameters for the message view.</param>
        /// <param name="owner">The message view will be attached to this owning window
        /// of this parameter is non-null, otherwise Application.Current.MainWindow
        /// is being used.</param>
        /// <returns></returns>
        private async Task<MWindowInterfacesLib.MsgBox.Enums.MsgBoxResult> Show(
            MsgBoxViewModel viewModel,
            object context = null)
        {
            Window owner = FindOwnerWindow(context);
            var metroWindow = owner as IMetroWindow;

            if (metroWindow != null)
            {
                var msgBoxDialog = new MsgBoxDialog(metroWindow);

                if (msgBoxDialog != null)
                {
                    // Get a view and bind datacontext to it
                    msgBoxDialog.PART_MessageScrollViewer.Content = new Views.MsgBoxView();
                    msgBoxDialog.DataContext = viewModel;

                    return await _DlgService.Manager.ShowMsgBoxAsync(metroWindow, msgBoxDialog);
                }
            }

            return MWindowInterfacesLib.MsgBox.Enums.MsgBoxResult.None;
        }

        /// <summary>
        /// Attempt to find the owner window for a message box
        /// </summary>
        /// <returns>Owner Window</returns>
        private Window GetOwnerWindow()
        {
            Window owner = null;

            if (Application.Current != null)
            {
                foreach (Window w in Application.Current.Windows)
                {
                    if (w != null)
                    {
                        if (w.IsActive)
                        {
                            owner = w;
                            break;
                        }
                    }
                }
            }

            return owner;
        }

        /// <summary>
        /// Construct a new message box viewmodel
        /// </summary>
        /// <returns></returns>
        private MsgBoxViewModel InitializeViewModel(
          Exception exp,
          string messageBoxText,
          string caption,
          string details,
          MsgBoxButtons buttonOption,
          MsgBoxImage image,
          MsgBoxResult btnDefault = MsgBoxResult.None,
          object helpLink = null,
          string helpLinkTitle = "",
          string helpLabel = "",
          Func<object, bool> navigateHelplinkMethod = null,
          bool enableCopyFunction = false,
          MsgBoxResult defaultCloseResult = MsgBoxResult.None,
          bool dialogCanCloseViaChrome = true)
        {
            if (exp == null)
            {
                // Construct the message box viewmodel WITHOUT System.Exception details
                var viewModel = new MsgBox.ViewModels.MsgBoxViewModel
                (
                  messageBoxText, caption, details,
                  buttonOption,
                  image,
                  btnDefault,
                  helpLink, helpLinkTitle, navigateHelplinkMethod,
                  enableCopyFunction,
                  defaultCloseResult, dialogCanCloseViaChrome
                );

                viewModel.HyperlinkLabel = helpLabel;

                return viewModel;
            }
            else
            {
                string sMess = MsgBox.Local.Strings.Unknown_Error_Message;
                messageBoxText = string.Empty;

                messageBoxText = MsgBoxViewModel.GetExceptionDetails(exp, messageBoxText, out sMess);

                // Construct the message box viewmodel WITH System.Exception details
                var viewModel = new MsgBox.ViewModels.MsgBoxViewModel
                (
                  messageBoxText, caption, sMess,
                  buttonOption, image, btnDefault,
                  helpLink, helpLinkTitle, navigateHelplinkMethod,
                  enableCopyFunction,
                  defaultCloseResult, dialogCanCloseViaChrome
                );

                viewModel.HyperlinkLabel = helpLabel;

                return viewModel;
            }
        }


        /// <summary>
        /// WRAP ASYNC CODE INTO STANDARD SYNC CODE for usage in WPF.
        /// 
        /// Implements a modal blocking behaviour for async methods in WPF
        /// and returns the result of the dialog.
        /// 
        /// https://social.msdn.microsoft.com/Forums/en-US/163ef755-ff7b-4ea5-b226-bbe8ef5f4796/is-there-a-pattern-for-calling-an-async-method-synchronously?forum=async&prof=required
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private MsgBoxResult WaitWithPumping(Task<MsgBoxResult> task)

        {
            if (task == null)
                throw new ArgumentNullException("Task cannot be null.");

            var nestedFrame = new DispatcherFrame();

            task.ContinueWith(_ => nestedFrame.Continue = false);

            Dispatcher.PushFrame(nestedFrame);

            task.Wait();

            return task.Result;
        }
        #endregion private methods
        #endregion methods
    }
}
