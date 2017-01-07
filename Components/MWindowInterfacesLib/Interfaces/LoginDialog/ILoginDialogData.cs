namespace MWindowInterfaceLib.Interfaces.LoginDialog
{
    public interface ILoginDialogData
    {
        string Password { get; }
        bool ShouldRemember { get; }
        string Username { get; }
    }
}