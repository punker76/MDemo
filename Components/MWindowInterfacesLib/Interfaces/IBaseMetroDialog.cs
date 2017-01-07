namespace MWindowInterfacesLib.Interfaces
{
    using Enums;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows;

    public interface IBaseMetroDialog
    {
        object DialogBottom { get; set; }
        double DialogMessageFontSize { get; set; }
        IMetroDialogSettings DialogSettings { get; }
        double DialogTitleFontSize { get; set; }
        object DialogTop { get; set; }
        string Title { get; set; }

        //
        // Summary:
        //     Gets or sets the maximum height constraint of the element.
        //
        // Returns:
        //     The maximum height of the element, in device-independent units (1/96th inch per
        //     unit). The default value is System.Double.PositiveInfinity. This value can be
        //     any value equal to or greater than 0.0. System.Double.PositiveInfinity is also
        //     valid.
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        [TypeConverter(typeof(LengthConverter))]
        double MaxHeight { get; set; }
        
        //
        // Summary:
        //     Gets or sets the maximum width constraint of the element.
        //
        // Returns:
        //     The maximum width of the element, in device-independent units (1/96th inch per
        //     unit). The default value is System.Double.PositiveInfinity. This value can be
        //     any value equal to or greater than 0.0. System.Double.PositiveInfinity is also
        //     valid.
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        [TypeConverter(typeof(LengthConverter))]
        double MaxWidth { get; set; }
        
        //
        // Summary:
        //     Gets or sets the minimum height constraint of the element.
        //
        // Returns:
        //     The minimum height of the element, in device-independent units (1/96th inch per
        //     unit). The default value is 0.0. This value can be any value equal to or greater
        //     than 0.0. However, System.Double.PositiveInfinity is NOT valid, nor is System.Double.NaN.
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        [TypeConverter(typeof(LengthConverter))]
        double MinHeight { get; set; }
        
        //
        // Summary:
        //     Gets or sets the minimum width constraint of the element.
        //
        // Returns:
        //     The minimum width of the element, in device-independent units (1/96th inch per
        //     unit). The default value is 0.0. This value can be any value equal to or greater
        //     than 0.0. However, System.Double.PositiveInfinity is not valid, nor is System.Double.NaN.
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        [TypeConverter(typeof(LengthConverter))]
        double MinWidth { get; set; }

        //
        // Summary:
        //     Gets or sets the data context for an element when it participates in data binding.
        //
        // Returns:
        //     The object to use as data context.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Localizability(LocalizationCategory.NeverLocalize)]
        object DataContext { get; set; }

        #region events
        SizeChangedEventHandler SizeChangedHandler { get; set; }
        #endregion events

        Task WaitForLoadAsync();
        Task WaitUntilUnloadedAsync();
        Task _WaitForCloseAsync();

        void OnClose();
        void OnShown();

        /// <summary>
        /// Set the ZIndex value for this <seealso cref="BaseMetroDialog"/>.
        /// This method can make sure that a given dialog is visible when more
        /// than one dialog is open.
        /// </summary>
        /// <param name="newPanelIndex"></param>
        void SetZIndex(int newPanelIndex);
   }

    public interface IMsgBoxDialog : IBaseMetroDialog
    {
        Task<MsgBox.Enums.MsgBoxResult> WaitForButtonPressAsync();
    }
}