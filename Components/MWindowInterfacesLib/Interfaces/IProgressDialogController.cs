namespace MWindowInterfacesLib.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IProgressDialogController
    {
        bool IsCanceled { get; }
        bool IsOpen { get; }
        double Maximum { get; set; }
        double Minimum { get; set; }

        event EventHandler Canceled;
        event EventHandler Closed;

        Task CloseAsync();
        void SetCancelable(bool value);
        void SetIndeterminate();
        void SetMessage(string message);
        void SetProgress(double value);
        void SetTitle(string title);
    }
}