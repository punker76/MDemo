namespace MWindowInterfacesLib.Interfaces
{
    using Enums;
    using System.Threading;
    using System.Windows;

    public interface IMetroDialogSettings
    {
        string AffirmativeButtonText { get; set; }

        bool AnimateHide { get; set; }

        bool AnimateShow { get; set; }

        CancellationToken CancellationToken { get; set; }

        ////MetroDialogColorScheme ColorScheme { get; set; }

        ResourceDictionary CustomResourceDictionary { get; set; }

        MessageDialogResult DefaultButtonFocus { get; set; }

        string DefaultText { get; set; }

        double DialogMessageFontSize { get; set; }

        double DialogTitleFontSize { get; set; }

        string FirstAuxiliaryButtonText { get; set; }

        double MaximumBodyHeight { get; set; }

        string NegativeButtonText { get; set; }

        string SecondAuxiliaryButtonText { get; set; }

        bool SuppressDefaultResources { get; set; }
    }
}