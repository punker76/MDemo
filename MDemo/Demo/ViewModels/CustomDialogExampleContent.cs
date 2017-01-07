namespace MDemo.Demo.ViewModels
{
    using MDemo.ViewModels.Base;
    using System;
    using System.Windows.Input;

    public class CustomDialogExampleContent : MDemo.ViewModels.Base.ViewModelBase
    {
        private ICommand _closeCommand;
        private Action<CustomDialogExampleContent> _closeHandler = null;

        private string _firstName;
        private string _lastName;

        public CustomDialogExampleContent(Action<CustomDialogExampleContent> closeHandler)
        {
            _closeHandler = closeHandler;
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => this.FirstName);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => this.LastName);
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(() =>
                    {
                        _closeHandler(this);
                    });
                }
                return _closeCommand;
            }
        }
    }
}
