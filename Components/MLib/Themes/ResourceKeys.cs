namespace MLib.Themes
{
    using System.Windows;

    public static class ResourceKeys
    {
        #region Accent Keys
        // Accent Color Key and Accent Brush Key
        // These keys are used to accent elements in the UI
        // (e.g.: Color of Activated Normal Window Frame, ResizeGrip, Focus or MouseOver input elements)
        public static readonly ComponentResourceKey ControlAccentColorKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlAccentColorKey");
        public static readonly ComponentResourceKey ControlAccentBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlAccentBrushKey");
        #endregion Accent Keys

        #region Normal Control Foreground and Background Keys
        // Color Keys
        public static readonly ComponentResourceKey ControlNormalForegroundKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlNormalForegroundKey");
        public static readonly ComponentResourceKey ControlNormalBackgroundKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlNormalBackgroundKey");

        // Brush Keys for colors defined above
        public static readonly ComponentResourceKey ControlNormalForegroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlNormalForegroundBrushKey");
        public static readonly ComponentResourceKey ControlNormalBackgroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlNormalBackgroundBrushKey");
        #endregion Normal Control Foreground and Background Keys

        #region MouseOver Keys
        public static readonly ComponentResourceKey ControlMouseOverBackgroundKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlMouseOverBackgroundKey");
        public static readonly ComponentResourceKey ControlMouseOverBackgroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlMouseOverBackgroundBrushKey");
        #endregion

        public static readonly ComponentResourceKey ControlTextBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlTextBrushKey");

        public static readonly ComponentResourceKey ControlCloseButtonWidthKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlCloseButtonWidthKey");
        public static readonly ComponentResourceKey ControlSystemButtonHeightKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlSystemButtonHeightKey");
        public static readonly ComponentResourceKey ControlSystemButtonWidthKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlSystemButtonWidthKey");

        public static readonly ComponentResourceKey ControlButtonTextKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlButtonTextKey");
        public static readonly ComponentResourceKey ControlSystemButtonBackgroundOnMoseOverKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlSystemButtonBackgroundOnMoseOverKey");
        public static readonly ComponentResourceKey ControlSystemButtonForegroundOnMoseOverKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlSystemButtonForegroundOnMoseOverKey");

        public static readonly ComponentResourceKey ControlButtonBackgroundKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlButtonBackgroundKey");
        public static readonly ComponentResourceKey ControlButtonBorderKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlButtonBorderKey");

        public static readonly ComponentResourceKey ControlSystemButtonBackgroundIsPressedKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlSystemButtonBackgroundIsPressedKey");
        public static readonly ComponentResourceKey ControlSystemButtonForegroundIsPressedKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlSystemButtonForegroundIsPressedKey");
        public static readonly ComponentResourceKey ControlButtonTextDisabledKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlButtonTextDisabledKey");

        public static readonly ComponentResourceKey ControlDisabledBackgroundKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlDisabledBackgroundKey");
        public static readonly ComponentResourceKey ControlDisabledBorderKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlDisabledBorderKey");

        public static readonly ComponentResourceKey ControlButtonBackgroundHoverKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlButtonBackgroundHoverKey");
        public static readonly ComponentResourceKey ControlButtonBackgroundPressedKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlButtonBackgroundPressedKey");
        public static readonly ComponentResourceKey ControlButtonBorderHoverKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlButtonBorderHoverKey");
        public static readonly ComponentResourceKey ControlButtonTextHoverKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlButtonTextHoverKey");
        public static readonly ComponentResourceKey ControlButtonTextPressedKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlButtonTextPressedKey");
        public static readonly ComponentResourceKey ControlButtonBorderPressedKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlButtonBorderPressedKey");
        public static readonly ComponentResourceKey ControlButtonIsDefaultBorderBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlButtonIsDefaultBorderBrushKey");

        // Color definitions for ItemsControl based controls (Listview, Combobox, listbox etc...)
        public static readonly ComponentResourceKey ControlItemTextKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlItemTextKey");
        public static readonly ComponentResourceKey ControlInputTextDisabledKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlInputTextDisabledKey");
        public static readonly ComponentResourceKey ControlItemBackgroundHoverKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlItemBackgroundHoverKey");
        public static readonly ComponentResourceKey ControlItemTextHoverKey    = new ComponentResourceKey(typeof(ResourceKeys), "ControlItemTextHoverKey");
        public static readonly ComponentResourceKey ControlItemTextSelectedKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlItemTextSelectedKey");
        public static readonly ComponentResourceKey ControlItemTextDisabledKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlItemTextDisabledKey");
        public static readonly ComponentResourceKey ControlItemBackgroundSelectedKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlItemBackgroundSelectedKey");
        public static readonly ComponentResourceKey ControlItemBorderSelectedKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlItemBorderSelectedKey");

        // Color definitions for ScrollBar specific items
        public static readonly ComponentResourceKey ControlScrollBarThumbBackgroundKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlScrollBarThumbBackgroundKey");
        public static readonly ComponentResourceKey ControlScrollBarThumbBorderKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlScrollBarThumbBorderKey");
        public static readonly ComponentResourceKey ControlScrollBarThumbForegroundKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlScrollBarThumbForegroundKey");
        public static readonly ComponentResourceKey ControlScrollBarThumbBackgroundHoverKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlScrollBarThumbBackgroundHoverKey");
        public static readonly ComponentResourceKey ControlScrollBarThumbForegroundHoverKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlScrollBarThumbForegroundHoverKey");
        public static readonly ComponentResourceKey ControlScrollBarThumbBackgroundDraggingKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlScrollBarThumbBackgroundDraggingKey");
        public static readonly ComponentResourceKey ControlScrollBarThumbForegroundDraggingKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlScrollBarThumbForegroundDraggingKey");
        public static readonly ComponentResourceKey ControlScrollBarBackgroundKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlScrollBarBackgroundKey");

        // Tool Tip Values
        public static readonly ComponentResourceKey ControlWindowBorderActiveKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlWindowBorderActiveKey");
        public static readonly ComponentResourceKey ControlWindowBackgroundKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlWindowBackgroundKey");
        public static readonly ComponentResourceKey ControlWindowTextKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlWindowTextKey");

        ////public static readonly ComponentResourceKey ControlDefaultFontFamilyKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlDefaultFontFamilyKey");
        ////public static readonly ComponentResourceKey ControlDefaultFontSizeKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlDefaultFontSizeKey");

        public static readonly ComponentResourceKey ControlInputBackgroundHoverKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlInputBackgroundHoverKey");
        public static readonly ComponentResourceKey ControlInputBorderHoverKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlInputBorderHoverKey");
        public static readonly ComponentResourceKey ControlInputTextKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlInputTextKey");
        public static readonly ComponentResourceKey ControlInputBackgroundKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlInputBackgroundKey");
        public static readonly ComponentResourceKey ControlInputBorderKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlInputBorderKey");

        public static readonly ComponentResourceKey ControlPopupBackgroundKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlPopupBackgroundKey");
        public static readonly ComponentResourceKey ControlPopupBackgroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlPopupBackgroundBrushKey");

        public static readonly ComponentResourceKey ProgressBackgroundBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "ProgressBackgroundBrushKey");

        // Color for displaying Warning Icons
        public static readonly ComponentResourceKey ControlsValidationBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "ControlsValidationBrushKey");

        public static readonly ComponentResourceKey HighlightBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "HighlightBrushKey");

        public static readonly ComponentResourceKey GrayNormalKey = new ComponentResourceKey(typeof(ResourceKeys), "GrayNormalKey");
        public static readonly ComponentResourceKey GrayHoverKey = new ComponentResourceKey(typeof(ResourceKeys), "GrayHoverKey");

        public static readonly ComponentResourceKey GrayNormalBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "GrayNormalBrushKey");
        public static readonly ComponentResourceKey GrayHoverBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "GrayHoverBrushKey");
    }
}
