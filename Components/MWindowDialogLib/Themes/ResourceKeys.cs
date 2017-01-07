namespace MWindowDialogLib.Themes
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

        public static readonly ComponentResourceKey MsgBoxMessageColorBrushKey = new ComponentResourceKey(typeof(ResourceKeys), "MsgBoxMessageColorBrushKey");
    }
}
