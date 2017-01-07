namespace MWindowLib
{
    using Controls;  //Extensions
    using Microsoft.Windows.Shell.Standard;
    using MWindowInterfacesLib.Interfaces;
    using Native;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    /// <summary>
    /// The <seealso cref="MetroWindow"/> class is a CustonControl that inherites from Window.
    /// 
    /// Remarks:
    /// CustomControl Window is based on this source:
    /// http://stackoverflow.com/questions/13592326/making-wpf-applications-look-metro-styled-even-in-windows-7-window-chrome-t
    /// 
    /// and of course MahApps.Metro http://mahapps.com/
    /// </summary>
    [TemplatePart(Name = PART_OverlayBox, Type = typeof(Grid))]
    [TemplatePart(Name = PART_MetroActiveDialogContainer, Type = typeof(Grid))]
    [TemplatePart(Name = PART_MetroInactiveDialogsContainer, Type = typeof(Grid))]
    [TemplatePart(Name = PART_WindowTitleThumb, Type = typeof(Thumb))]
    public class MetroWindow : Window, IMetroWindow
    {
        #region fields
        internal Grid _OverlayBox;
        internal Grid _MetroActiveDialogContainer;
        internal Grid _MetroInactiveDialogContainer;

        private const string PART_OverlayBox = "PART_OverlayBox";
        private const string PART_MetroActiveDialogContainer = "PART_MetroActiveDialogContainer";
        private const string PART_MetroInactiveDialogsContainer = "PART_MetroInactiveDialogsContainer";
        private const string PART_WindowTitleThumb = "PART_WindowTitleThumb";

        private IInputElement _RestoreFocus;
        private Storyboard _OverlayStoryboard;
        private Thumb _WindowTitleThumb;
        #endregion fields

        #region ctor
        /// <summary>
        /// Static constructor
        /// </summary>
        static MetroWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroWindow), new FrameworkPropertyMetadata(typeof(MetroWindow)));
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        public MetroWindow()
        {
            this.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, this.OnCloseWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, this.OnMaximizeWindow, this.OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, this.OnMinimizeWindow, this.OnCanMinimizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, this.OnRestoreWindow, this.OnCanResizeWindow));

            this.SourceInitialized += new EventHandler(Window1_SourceInitialized);
        }
        #endregion ctor

        #region properties
        public Grid OverlayBox
        {
            get { return _OverlayBox; }
        }

        public Grid MetroActiveDialogContainer
        {
            get { return _MetroActiveDialogContainer; }
        }

        public Grid MetroInactiveDialogContainer
        {
            get { return _MetroInactiveDialogContainer; }
        }

        public static readonly DependencyProperty MetroDialogOptionsProperty =
            DependencyProperty.Register("MetroDialogOptions"
                  , typeof(MWindowInterfacesLib.Interfaces.IMetroDialogSettings)
                  , typeof(MetroWindow)
                , new PropertyMetadata(new MWindowInterfacesLib.MetroDialogSettings()));

        public IMetroDialogSettings MetroDialogOptions
        {
            get { return (IMetroDialogSettings)GetValue(MetroDialogOptionsProperty); }
            set { SetValue(MetroDialogOptionsProperty, value); }
        }

        #region Window Icon
        public static readonly DependencyProperty ShowIconProperty = DependencyProperty.Register("ShowIcon", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        /// <summary>
        /// Gets/sets if the close button is visible.
        /// </summary>
        public bool ShowIcon
        {
            get { return (bool)GetValue(ShowIconProperty); }
            set { SetValue(ShowIconProperty, value); }
        }
        #endregion Window Icon

        #region Window Title
        public static readonly DependencyProperty ShowTitleProperty = DependencyProperty.Register("ShowTitle", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        /// <summary>
        /// Gets/sets if the close button is visible.
        /// </summary>
        public bool ShowTitle
        {
            get { return (bool)GetValue(ShowTitleProperty); }
            set { SetValue(ShowTitleProperty, value); }
        }
        #endregion Window Title

        #region Window Min Button
        public static readonly DependencyProperty ShowMinButtonProperty = DependencyProperty.Register("ShowMinButton", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        /// <summary>
        /// Gets/sets if the close button is visible.
        /// </summary>
        public bool ShowMinButton
        {
            get { return (bool)GetValue(ShowMinButtonProperty); }
            set { SetValue(ShowMinButtonProperty, value); }
        }
        #endregion Window Min Button

        #region Window Max Button
        public static readonly DependencyProperty ShowMaxButtonProperty = DependencyProperty.Register("ShowMaxButton", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        /// <summary>
        /// Gets/sets if the close button is visible.
        /// </summary>
        public bool ShowMaxButton
        {
            get { return (bool)GetValue(ShowMaxButtonProperty); }
            set { SetValue(ShowMaxButtonProperty, value); }
        }
        #endregion Window Max Button

        #region Window Close Button
        public static readonly DependencyProperty ShowCloseButtonProperty = DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        /// <summary>
        /// Gets/sets if the close button is visible.
        /// </summary>
        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }
        #endregion Window Close Button

        #region Show TitleBar
        /// <summary>
        /// Gets/sets whether the TitleBar is visible or not.
        /// </summary>
        public bool ShowTitleBar
        {
            get { return (bool)GetValue(ShowTitleBarProperty); }
            set { SetValue(ShowTitleBarProperty, value); }
        }

        private static void OnShowTitleBarPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (MetroWindow)d;
            if (e.NewValue != e.OldValue)
            {
                window.SetVisibiltyForAllTitleElements((bool)e.NewValue);
            }
        }

        private static object OnShowTitleBarCoerceValueCallback(DependencyObject d, object value)
        {
            // if UseNoneWindowStyle = true no title bar should be shown
            if (((MetroWindow)d).UseNoneWindowStyle)
            {
                return false;
            }
            return value;
        }
        #endregion Show TitleBar

        public static readonly DependencyProperty UseNoneWindowStyleProperty = DependencyProperty.Register("UseNoneWindowStyle", typeof(bool), typeof(MetroWindow), new PropertyMetadata(false, OnUseNoneWindowStylePropertyChangedCallback));

        /// <summary>
        /// Gets/sets whether the WindowStyle is None or not.
        /// </summary>
        public bool UseNoneWindowStyle
        {
            get { return (bool)GetValue(UseNoneWindowStyleProperty); }
            set { SetValue(UseNoneWindowStyleProperty, value); }
        }

        private static void OnUseNoneWindowStylePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                // if UseNoneWindowStyle = true no title bar should be shown
                var useNoneWindowStyle = (bool)e.NewValue;
                var window = (MetroWindow)d;
                window.ToggleNoneWindowStyle(useNoneWindowStyle);
            }
        }

        private void ToggleNoneWindowStyle(bool useNoneWindowStyle)
        {
            // UseNoneWindowStyle means no title bar, window commands or min, max, close buttons
            if (useNoneWindowStyle)
            {
                ShowTitleBar = false;
            }

////            if (LeftWindowCommandsPresenter != null)
////            {
////                LeftWindowCommandsPresenter.Visibility = useNoneWindowStyle ? Visibility.Collapsed : Visibility.Visible;
////            }
////            if (RightWindowCommandsPresenter != null)
////            {
////                RightWindowCommandsPresenter.Visibility = useNoneWindowStyle ? Visibility.Collapsed : Visibility.Visible;
////            }
        }

        public static readonly DependencyProperty ShowTitleBarProperty = DependencyProperty.Register("ShowTitleBar", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true, OnShowTitleBarPropertyChangedCallback, OnShowTitleBarCoerceValueCallback));

        public static readonly DependencyProperty ShowDialogsOverTitleBarProperty = DependencyProperty.Register("ShowDialogsOverTitleBar", typeof(bool), typeof(MetroWindow), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Get/sets whether dialogs show over the title bar.
        /// </summary>
        public bool ShowDialogsOverTitleBar
        {
            get { return (bool)GetValue(ShowDialogsOverTitleBarProperty); }
            set { SetValue(ShowDialogsOverTitleBarProperty, value); }
        }

        public static readonly DependencyProperty GlowBrushProperty = DependencyProperty.Register("GlowBrush", typeof(Brush), typeof(MetroWindow), new PropertyMetadata(null));

        /// <summary>
        /// Gets/sets the brush used for the Window's glow.
        /// </summary>
        public Brush GlowBrush
        {
            get { return (Brush)GetValue(GlowBrushProperty); }
            set { SetValue(GlowBrushProperty, value); }
        }


        public static readonly DependencyProperty IsWindowDraggableProperty = DependencyProperty.Register("IsWindowDraggable", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        public bool IsWindowDraggable
        {
            get { return (bool)GetValue(IsWindowDraggableProperty); }
            set { SetValue(IsWindowDraggableProperty, value); }
        }

        public static readonly DependencyProperty ShowSystemMenuOnRightClickProperty = DependencyProperty.Register("ShowSystemMenuOnRightClick", typeof(bool), typeof(MetroWindow), new PropertyMetadata(true));

        /// <summary>
        /// Gets/sets if the the system menu should popup on right click.
        /// </summary>
        public bool ShowSystemMenuOnRightClick
        {
            get { return (bool)GetValue(ShowSystemMenuOnRightClickProperty); }
            set { SetValue(ShowSystemMenuOnRightClickProperty, value); }
        }
        #endregion properties

        #region methodes
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _OverlayBox = GetTemplateChild(PART_OverlayBox) as Grid;
            _MetroActiveDialogContainer = GetTemplateChild(PART_MetroActiveDialogContainer) as Grid;
            _MetroInactiveDialogContainer = GetTemplateChild(PART_MetroInactiveDialogsContainer) as Grid;
            _WindowTitleThumb = GetTemplateChild(PART_WindowTitleThumb) as Thumb;

            SetVisibiltyForAllTitleElements(true); // this.TitlebarHeight > 0
        }

        #region Overlay Methods

        /// <summary>
        /// Begins to show the MetroWindow's overlay effect.
        /// </summary>
        /// <returns>A task representing the process.</returns>
        public System.Threading.Tasks.Task ShowOverlayAsync()
        {
            if (_OverlayBox == null) throw new InvalidOperationException("OverlayBox can not be founded in this MetroWindow's template. Are you calling this before the window has loaded?");

            var tcs = new System.Threading.Tasks.TaskCompletionSource<object>();

            if (IsOverlayVisible() && _OverlayStoryboard == null)
            {
                //No Task.FromResult in .NET 4.
                tcs.SetResult(null);
                return tcs.Task;
            }

            Dispatcher.VerifyAccess();

            _OverlayBox.Visibility = Visibility.Visible;

            var sb = (Storyboard)this.Template.Resources["OverlayFastSemiFadeIn"];

            sb = sb.Clone();

            EventHandler completionHandler = null;
            completionHandler = (sender, args) =>
            {
                sb.Completed -= completionHandler;

                if (_OverlayStoryboard == sb)
                {
                    _OverlayStoryboard = null;
                }

                tcs.TrySetResult(null);
            };

            sb.Completed += completionHandler;

            _OverlayBox.BeginStoryboard(sb);

            _OverlayStoryboard = sb;

            return tcs.Task;
        }

        /// <summary>
        /// Begins to hide the MetroWindow's overlay effect.
        /// </summary>
        /// <returns>A task representing the process.</returns>
        public System.Threading.Tasks.Task HideOverlayAsync()
        {
            if (_OverlayBox == null) throw new InvalidOperationException("OverlayBox can not be founded in this MetroWindow's template. Are you calling this before the window has loaded?");

            var tcs = new System.Threading.Tasks.TaskCompletionSource<object>();

            if (_OverlayBox.Visibility == Visibility.Visible && _OverlayBox.Opacity == 0.0)
            {
                //No Task.FromResult in .NET 4.
                tcs.SetResult(null);
                return tcs.Task;
            }

            Dispatcher.VerifyAccess();

            var sb = (Storyboard)this.Template.Resources["OverlayFastSemiFadeOut"];

            sb = sb.Clone();

            EventHandler completionHandler = null;
            completionHandler = (sender, args) =>
            {
                sb.Completed -= completionHandler;

                if (_OverlayStoryboard == sb)
                {
                    _OverlayBox.Visibility = Visibility.Hidden;
                    _OverlayStoryboard = null;
                }

                tcs.TrySetResult(null);
            };

            sb.Completed += completionHandler;

            _OverlayBox.BeginStoryboard(sb);

            _OverlayStoryboard = sb;

            return tcs.Task;
        }

        public bool IsOverlayVisible()
        {
            if (_OverlayBox == null) throw new InvalidOperationException("OverlayBox can not be founded in this MetroWindow's template. Are you calling this before the window has loaded?");

            return _OverlayBox.Visibility == Visibility.Visible && _OverlayBox.Opacity >= 0.7;
        }

        public void ShowOverlay()
        {
            _OverlayBox.Visibility = Visibility.Visible;
            //overlayBox.Opacity = 0.7;
            _OverlayBox.SetCurrentValue(Grid.OpacityProperty, 0.7);
        }
        public void HideOverlay()
        {
            //overlayBox.Opacity = 0.0;
            _OverlayBox.SetCurrentValue(Grid.OpacityProperty, 0.0);
            _OverlayBox.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Stores the given element, or the last focused element via FocusManager, for restoring the focus after closing a dialog.
        /// </summary>
        /// <param name="thisElement">The element which will be focused again.</param>
        public void StoreFocus(IInputElement thisElement = null) // [CanBeNull] 
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _RestoreFocus = thisElement ?? (this._RestoreFocus ?? FocusManager.GetFocusedElement(this));
            }));
        }

        public void RestoreFocus()
        {
            if (_RestoreFocus != null)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Keyboard.Focus(_RestoreFocus);
                    _RestoreFocus = null;
                }));
            }
        }

        /// <summary>
        /// Clears the stored element which would get the focus after closing a dialog.
        /// </summary>
        public void ResetStoredFocus()
        {
            _RestoreFocus = null;
        }
        #endregion Overlay Methods
/***
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
        public Task<MessageDialogResult> ShowMessageAsync(string title
            , string message
            , MessageDialogStyle style = MessageDialogStyle.Affirmative
            , IMetroDialogSettings settings = null)
        {
            this.Dispatcher.VerifyAccess();
            return this.HandleOverlayOnShow(settings).ContinueWith(z =>
            {
                return (Task<MessageDialogResult>)this.Dispatcher.Invoke(new Func<Task<MessageDialogResult>>(() =>
                {
                    settings = settings ?? this.MetroDialogOptions;

                    // create the dialog control
                    var dialog = new MessageDialog(this, settings)
                    {
                        Message = message,
                        Title = title,
                        ButtonStyle = style
                    };

                    SetDialogFontSizes(settings, dialog);

                    SizeChangedEventHandler sizeHandler = this.SetupAndOpenDialog(dialog);
                    dialog.SizeChangedHandler = sizeHandler;

                    return dialog.WaitForLoadAsync().ContinueWith(x =>
                    {
                        if (DialogOpened != null)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() => DialogOpened(this, new DialogStateChangedEventArgs())));
                        }

                        return dialog.WaitForButtonPressAsync().ContinueWith(y =>
                        {
                            //once a button as been clicked, begin removing the dialog.

                            dialog.OnClose();

                            if (DialogClosed != null)
                            {
                                this.Dispatcher.BeginInvoke(new Action(() => DialogClosed(this, new DialogStateChangedEventArgs())));
                            }

                            Task closingTask = (Task)this.Dispatcher.Invoke(new Func<Task>(() => dialog._WaitForCloseAsync()));
                            return closingTask.ContinueWith(a =>
                            {
                                return ((Task)this.Dispatcher.Invoke(new Func<Task>(() =>
                                {
                                    this.SizeChanged -= sizeHandler;

                                    this.RemoveDialog(dialog);

                                    return this.HandleOverlayOnHide(settings);
                                }))).ContinueWith(y3 => y).Unwrap();
                            });
                        }).Unwrap();
                    }).Unwrap().Unwrap();
                }));
            }).Unwrap();
        }

        /// <summary>
        /// Creates a ProgressDialog inside of the current window.
        /// </summary>
        /// <param name="window">The MetroWindow</param>
        /// <param name="title">The title of the ProgressDialog.</param>
        /// <param name="message">The message within the ProgressDialog.</param>
        /// <param name="isCancelable">Determines if the cancel button is visible.</param>
        /// <param name="settings">Optional Settings that override the global metro dialog settings.</param>
        /// <returns>A task promising the instance of ProgressDialogController for this operation.</returns>
        public Task<IProgressDialogController> ShowProgressAsync(
              string title
            , string message
            , bool isCancelable = false
            , IMetroDialogSettings settings = null)
        {
            this.Dispatcher.VerifyAccess();

            return this.HandleOverlayOnShow(settings).ContinueWith(z =>
            {
                return ((Task<IProgressDialogController>)this.Dispatcher.Invoke(new Func<Task<IProgressDialogController>>(() =>
                {
                    settings = settings ?? this.MetroDialogOptions;

                    //create the dialog control
                    var dialog = new ProgressDialog(this, settings)
                    {
                        Title = title,
                        Message = message,
                        IsCancelable = isCancelable
                    };

                    this.SetDialogFontSizes(settings, dialog);

                    SizeChangedEventHandler sizeHandler = this.SetupAndOpenDialog(dialog);
                    dialog.SizeChangedHandler = sizeHandler;

                    return dialog.WaitForLoadAsync().ContinueWith(x =>
                    {
                        if (DialogOpened != null)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() => DialogOpened(this, new DialogStateChangedEventArgs())));
                        }

                        return ProgressDialogController.Create(dialog, () =>
                        {
                            dialog.OnClose();

                            if (DialogClosed != null)
                            {
                                this.Dispatcher.BeginInvoke(new Action(() => DialogClosed(this, new DialogStateChangedEventArgs())));
                            }

                            Task closingTask = (Task)this.Dispatcher.Invoke(new Func<Task>(() => dialog._WaitForCloseAsync()));
                            return closingTask.ContinueWith(a =>
                            {
                                return (Task)this.Dispatcher.Invoke(new Func<Task>(() =>
                                {
                                    this.SizeChanged -= sizeHandler;

                                    this.RemoveDialog(dialog);

                                    return this.HandleOverlayOnHide(settings);
                                }));
                            }).Unwrap();
                        });
                    });
                })));
            }).Unwrap();
        }

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
        public Task HideMetroDialogAsync(
              IBaseMetroDialog dialog
            , IMetroDialogSettings settings = null)
        {
            this.Dispatcher.VerifyAccess();
            if (!this._MetroActiveDialogContainer.Children.Contains(dialog as UIElement) && !this._MetroInactiveDialogContainer.Children.Contains(dialog as UIElement))
                throw new InvalidOperationException("The provided dialog is not visible in the specified window.");

            this.SizeChanged -= dialog.SizeChangedHandler;

            dialog.OnClose();

            Task closingTask = (Task)this.Dispatcher.Invoke(new Func<Task>(dialog._WaitForCloseAsync));
            return closingTask.ContinueWith(a =>
            {
                if (DialogClosed != null)
                {
                    this.Dispatcher.BeginInvoke(new Action(() => DialogClosed(this, new DialogStateChangedEventArgs())));
                }

                return (Task)this.Dispatcher.Invoke(new Func<Task>(() =>
                {
                    this.RemoveDialog(dialog);

                    return this.HandleOverlayOnHide(settings);
                }));
            }).Unwrap();
        }
***/
        /// <summary>
        /// Gets the current shown dialog in async way.
        /// </summary>
        /// <param name="window">The dialog owner.</param>
        public Task<TDialog> GetCurrentDialogAsync<TDialog>() where TDialog : IBaseMetroDialog
        {
            this.Dispatcher.VerifyAccess();
            var t = new TaskCompletionSource<TDialog>();
            this.Dispatcher.Invoke((Action)(() =>
            {
                TDialog dialog = default(TDialog);

                if (this.MetroActiveDialogContainer != null)
                {
                    dialog = this.MetroActiveDialogContainer.Children.OfType<TDialog>().LastOrDefault();
                    t.TrySetResult(dialog);
                }
            }));
            return t.Task;
        }

/***
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
        public Task ShowMetroDialogAsync(IBaseMetroDialog dialog,
            IMetroDialogSettings settings = null)
        {
            this.Dispatcher.VerifyAccess();
            if (this._MetroActiveDialogContainer.Children.Contains(dialog as UIElement) || this._MetroInactiveDialogContainer.Children.Contains(dialog as UIElement))
                throw new InvalidOperationException("The provided dialog is already visible in the specified window.");

            return this.HandleOverlayOnShow(settings).ContinueWith(z =>
            {
                return (Task)this.Dispatcher.Invoke(new Func<Task>(() =>
                {
                    settings = settings ?? this.MetroDialogOptions;

                    SetDialogFontSizes(settings, dialog);

                    SizeChangedEventHandler sizeHandler = this.SetupAndOpenDialog(dialog);
                    dialog.SizeChangedHandler = sizeHandler;

                    return dialog.WaitForLoadAsync().ContinueWith(x =>
                    {
                        dialog.OnShown();

                        if (DialogOpened != null)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() => DialogOpened(this, new DialogStateChangedEventArgs())));
                        }
                    });
                }));
            }).Unwrap();
        }

        private void AddDialog(IBaseMetroDialog dialog)
        {
            this.StoreFocus();

            // if there's already an active dialog, move to the background
            var activeDialog = this._MetroActiveDialogContainer.Children.Cast<UIElement>().SingleOrDefault();
            if (activeDialog != null)
            {
                this._MetroActiveDialogContainer.Children.Remove(activeDialog);
                this._MetroInactiveDialogContainer.Children.Add(activeDialog);
            }

            // add the dialog to the container}
            this._MetroActiveDialogContainer.Children.Add(dialog as UIElement);
        }

        private void RemoveDialog(IBaseMetroDialog dialog)
        {
            if (this._MetroActiveDialogContainer.Children.Contains(dialog as UIElement))
            {
                // remove the dialog from the container
                this._MetroActiveDialogContainer.Children.Remove(dialog as UIElement);

                // if there's an inactive dialog, bring it to the front
                var dlg = this._MetroInactiveDialogContainer.Children.Cast<UIElement>().LastOrDefault();
                if (dlg != null)
                {
                    this._MetroInactiveDialogContainer.Children.Remove(dlg);
                    this._MetroActiveDialogContainer.Children.Add(dlg);
                }
            }
            else
            {
                this._MetroInactiveDialogContainer.Children.Remove(dialog as UIElement);
            }

            if (this._MetroActiveDialogContainer.Children.Count == 0)
            {
                this.RestoreFocus();
            }
        }

        private Task HandleOverlayOnHide(IMetroDialogSettings settings)
        {
            if (!this._MetroActiveDialogContainer.Children.OfType<IBaseMetroDialog>().Any())
            {
                return (settings == null || settings.AnimateHide ? this.HideOverlayAsync() : Task.Factory.StartNew(() => this.Dispatcher.Invoke(new Action(this.HideOverlay))));
            }
            else
            {
                var tcs = new System.Threading.Tasks.TaskCompletionSource<object>();
                tcs.SetResult(null);
                return tcs.Task;
            }
        }

        private Task HandleOverlayOnShow(IMetroDialogSettings settings)
        {
            if (!this._MetroActiveDialogContainer.Children.OfType<IBaseMetroDialog>().Any())
            {
                return (settings == null || settings.AnimateShow ? this.ShowOverlayAsync() : Task.Factory.StartNew(() => this.Dispatcher.Invoke(new Action(this.ShowOverlay))));
            }
            else
            {
                var tcs = new System.Threading.Tasks.TaskCompletionSource<object>();
                tcs.SetResult(null);
                return tcs.Task;
            }
        }

        private void SetDialogFontSizes(IMetroDialogSettings settings, IBaseMetroDialog dialog)
        {
            if (settings == null)
            {
                return;
            }

            if (!double.IsNaN(settings.DialogTitleFontSize))
            {
                dialog.DialogTitleFontSize = settings.DialogTitleFontSize;
            }

            if (!double.IsNaN(settings.DialogMessageFontSize))
            {
                dialog.DialogMessageFontSize = settings.DialogMessageFontSize;
            }
        }

        private SizeChangedEventHandler SetupAndOpenDialog(IBaseMetroDialog dialog)
        {
            dialog.SetValue(Panel.ZIndexProperty, (int)this._OverlayBox.GetValue(Panel.ZIndexProperty) + 1);
            dialog.MinHeight = this.ActualHeight / 4.0;
            dialog.MaxHeight = this.ActualHeight;

            SizeChangedEventHandler sizeHandler = (sender, args) =>
            {
                dialog.MinHeight = this.ActualHeight / 4.0;
                dialog.MaxHeight = this.ActualHeight;
            };

            this.SizeChanged += sizeHandler;

            this.AddDialog(dialog);

            dialog.OnShown();

            return sizeHandler;
        }
        #endregion DialogManager
***/
        /// <summary>
        /// Gets the template child with the given name.
        /// </summary>
        /// <typeparam name="T">The interface type inheirted from DependencyObject.</typeparam>
        /// <param name="name">The name of the template child.</param>
        internal T GetPart<T>(string name) where T : class
        {
            return GetTemplateChild(name) as T;
        }

        /// <summary>
        /// Gets the template child with the given name.
        /// </summary>
        /// <param name="name">The name of the template child.</param>
        internal DependencyObject GetPart(string name)
        {
            return GetTemplateChild(name);
        }

#if NET4_5
        protected override async void OnClosing(CancelEventArgs e)
        {
            // #2409: don't close window if there is a dialog still open
            var dialog = await this.GetCurrentDialogAsync<IBaseMetroDialog>();
            e.Cancel = dialog != null;
            base.OnClosing(e);
        }
#else
        protected override void OnClosing(CancelEventArgs e)
        {
            // #2409: don't close window if there is a dialog still open
            var dialog = this.Invoke(() => this.metroActiveDialogContainer?.Children.OfType<BaseMetroDialog>().LastOrDefault());
            e.Cancel = dialog != null;
            base.OnClosing(e);
        }
#endif

        protected IntPtr CriticalHandle
        {
            get
            {
                var value = typeof(Window)
                    .GetProperty("CriticalHandle", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(this, new object[0]);
                return (IntPtr)value;
            }
        }

        private void SetVisibiltyForAllTitleElements(bool visible)
        {

            SetWindowEvents();
        }

        private void SetWindowEvents()
        {
            // clear all event handlers first
            this.ClearWindowEvents();

            if (this._WindowTitleThumb != null)
            {
                this._WindowTitleThumb.PreviewMouseLeftButtonUp += WindowTitleThumbOnPreviewMouseLeftButtonUp;
                this._WindowTitleThumb.DragDelta += this.WindowTitleThumbMoveOnDragDelta;
                this._WindowTitleThumb.MouseDoubleClick += this.WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                this._WindowTitleThumb.MouseRightButtonUp += this.WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
        }

        private void ClearWindowEvents()
        {
            // clear all event handlers first:
            if (this._WindowTitleThumb != null)
            {
                this._WindowTitleThumb.PreviewMouseLeftButtonUp -= this.WindowTitleThumbOnPreviewMouseLeftButtonUp;
                this._WindowTitleThumb.DragDelta -= this.WindowTitleThumbMoveOnDragDelta;
                this._WindowTitleThumb.MouseDoubleClick -= this.WindowTitleThumbChangeWindowStateOnMouseDoubleClick;
                this._WindowTitleThumb.MouseRightButtonUp -= this.WindowTitleThumbSystemMenuOnMouseRightButtonUp;
            }
        }

        #region WindowTitleThumbEvents
        internal static void DoWindowTitleThumbOnPreviewMouseLeftButtonUp(MetroWindow window, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (mouseButtonEventArgs.Source == mouseButtonEventArgs.OriginalSource)
            {
                Mouse.Capture(null);
            }
        }

        internal static void DoWindowTitleThumbMoveOnDragDelta(IMetroThumb thumb, MetroWindow window, DragDeltaEventArgs dragDeltaEventArgs)
        {
            if (thumb == null)
            {
                throw new ArgumentNullException(nameof(thumb));
            }
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            // drag only if IsWindowDraggable is set to true
            if (!window.IsWindowDraggable ||
                (!(Math.Abs(dragDeltaEventArgs.HorizontalChange) > 2) && !(Math.Abs(dragDeltaEventArgs.VerticalChange) > 2)))
            {
                return;
            }

            // tage from DragMove internal code
            window.VerifyAccess();

            var cursorPos = NativeMethods.GetCursorPos();

            // if the window is maximized dragging is only allowed on title bar (also if not visible)
            var windowIsMaximized = window.WindowState == WindowState.Maximized;
            ////var isMouseOnTitlebar = Mouse.GetPosition(thumb).Y <= window.TitlebarHeight && window.TitlebarHeight > 0;
            ////if (!isMouseOnTitlebar && windowIsMaximized)
            ////{
            ////    return;
            ////}

            // for the touch usage
            UnsafeNativeMethods.ReleaseCapture();

            if (windowIsMaximized)
            {
                var cursorXPos = cursorPos.x;
                EventHandler windowOnStateChanged = null;
                windowOnStateChanged = (sender, args) =>
                {
                    //window.Top = 2;
                    //window.Left = Math.Max(cursorXPos - window.RestoreBounds.Width / 2, 0);

                    window.StateChanged -= windowOnStateChanged;
                    if (window.WindowState == WindowState.Normal)
                    {
                        Mouse.Capture(thumb, CaptureMode.Element);
                    }
                };
                window.StateChanged += windowOnStateChanged;
            }

            var criticalHandle = window.CriticalHandle;
            // DragMove works too
            // window.DragMove();
            // instead this 2 lines
            NativeMethods.SendMessage(criticalHandle, WM.SYSCOMMAND, (IntPtr)SC.MOUSEMOVE, IntPtr.Zero);
            NativeMethods.SendMessage(criticalHandle, WM.LBUTTONUP, IntPtr.Zero, IntPtr.Zero);
        }

        internal static void DoWindowTitleThumbChangeWindowStateOnMouseDoubleClick(MetroWindow window, MouseButtonEventArgs mouseButtonEventArgs)
        {
            // restore/maximize only with left button
            if (mouseButtonEventArgs.ChangedButton == MouseButton.Left)
            {
                // we can maximize or restore the window if the title bar height is set (also if title bar is hidden)
                var canResize = window.ResizeMode == ResizeMode.CanResizeWithGrip || window.ResizeMode == ResizeMode.CanResize;
                var mousePos = Mouse.GetPosition(window);
                var isMouseOnTitlebar = true; //// var isMouseOnTitlebar = mousePos.Y <= window.TitlebarHeight && window.TitlebarHeight > 0;
                if (canResize && isMouseOnTitlebar)
                {
                    if (window.WindowState == WindowState.Maximized)
                    {
                        SystemCommands.RestoreWindow(window);
                    }
                    else
                    {
                        SystemCommands.MaximizeWindow(window);
                    }
                    mouseButtonEventArgs.Handled = true;
                }
            }
        }
        private static void ShowSystemMenuPhysicalCoordinates(Window window, Point physicalScreenLocation)
        {
            if (window == null) return;

            var hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero || !UnsafeNativeMethods.IsWindow(hwnd))
                return;

            var hmenu = UnsafeNativeMethods.GetSystemMenu(hwnd, false);

            var cmd = UnsafeNativeMethods.TrackPopupMenuEx(hmenu, Constants.TPM_LEFTBUTTON | Constants.TPM_RETURNCMD,
                (int)physicalScreenLocation.X, (int)physicalScreenLocation.Y, hwnd, IntPtr.Zero);
            if (0 != cmd)
                UnsafeNativeMethods.PostMessage(hwnd, Constants.SYSCOMMAND, new IntPtr(cmd), IntPtr.Zero);
        }

        internal static void DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(MetroWindow window, MouseButtonEventArgs e)
        {
            if (window.ShowSystemMenuOnRightClick)
            {
                // show menu only if mouse pos is on title bar or if we have a window with none style and no title bar
                var mousePos = e.GetPosition(window);
                ////if ((mousePos.Y <= window.TitlebarHeight && window.TitlebarHeight > 0) || (window.UseNoneWindowStyle && window.TitlebarHeight <= 0))
                ////{
                    ShowSystemMenuPhysicalCoordinates(window, window.PointToScreen(mousePos));
                ////}
            }
        }

        private void WindowTitleThumbOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DoWindowTitleThumbOnPreviewMouseLeftButtonUp(this, e);
        }

        private void WindowTitleThumbMoveOnDragDelta(object sender, DragDeltaEventArgs dragDeltaEventArgs)
        {
            DoWindowTitleThumbMoveOnDragDelta(sender as IMetroThumb, this, dragDeltaEventArgs);
        }

        private void WindowTitleThumbChangeWindowStateOnMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            DoWindowTitleThumbChangeWindowStateOnMouseDoubleClick(this, mouseButtonEventArgs);
        }

        private void WindowTitleThumbSystemMenuOnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DoWindowTitleThumbSystemMenuOnMouseRightButtonUp(this, e);
        }
        #endregion WindowTitleThumbEvents

        private void UseDropShadow()
        {
            this.BorderThickness = new Thickness(0);
            this.BorderBrush = null;
            this.GlowBrush = Brushes.Black;
        }

        private void Window1_SourceInitialized(object sender, EventArgs e)
        {
            Util.WindowSizing.WindowInitialized(this);
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode != ResizeMode.NoResize;
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }
        #endregion methodes
    }
}
