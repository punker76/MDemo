namespace MWindowDialogLib.Internal
{
    using MWindowInterfaceLib.Interfaces.LoginDialog;

    internal class LoginDialogData : ILoginDialogData
    {
        public string Username { get; internal set; }
        public string Password { get; internal set; }
        public bool ShouldRemember { get; internal set; }
    }
}
