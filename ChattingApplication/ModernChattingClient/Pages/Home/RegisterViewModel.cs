using ChattingInterfaces;
using ModernChattingClient.Base;
using ModernChattingClient.ClientServices;
using System;
using System.ServiceModel;

namespace ModernChattingClient.Pages.Home
{
    public class RegisterViewModel : BindableBase
    {
        private static IChattingService _server;

        public RegisterViewModel()
        {
            var channelFactory = new DuplexChannelFactory<IChattingService>(new ClientService(), "ChattingServiceEndPoint");
            _server = channelFactory.CreateChannel();
            Register = new RelayCommand(OnRegister, () => !(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password)));
            ClearCommand = new RelayCommand(OnClear);            
        }

        private string _username;
        private string _password;

        public string UserName
        {
            get { return _username; }
            set
            {
                SetProperty(ref _username, value);
                UserNameBorder = "Gray";
                OnPropertyChanged("UserName");
                Register.RaiseCanExecuteChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                PasswordBorder = "Gray";
                OnPropertyChanged("Password");
                Register.RaiseCanExecuteChanged();
            }
        }

        private string _usernameborder = "Gray";
        private string _passwordborder = "Gray";
        private ReturnMessages _returnmessage = new ReturnMessages();
        private string _regmessage;

        public string RegMessage
        {
            get
            {
                return _regmessage;
            }
            set
            {
                SetProperty(ref _regmessage, value);
            }
        }

        public string UserNameBorder
        {
            get
            {
                return _usernameborder;
            }
            set
            {
                SetProperty(ref _usernameborder, value);
            }
        }
        public string PasswordBorder
        {
            get
            {
                return _passwordborder;
            }
            set
            {
                SetProperty(ref _passwordborder, value);
            }
        }
        public ReturnMessages ReturnMessage
        {
            get
            {
                return _returnmessage;
            }
            set
            {
                SetProperty(ref _returnmessage, value);
            }
        }

        public RelayCommand Register { get; }
        public void OnRegister()
        {

            try
            {
                if (_server.Register(UserName, Password))
                {
                    ReturnMessage.RegisterMessage = "Successful Registered, Your Username: " + UserName;
                    ReturnMessage.RegisterColor = "Green";

                }
                else
                {
                    ReturnMessage.RegisterMessage = "Username already taken";
                    ReturnMessage.RegisterColor = "Red";
                }
            }
            catch (Exception)
            {
                ReturnMessage.RegisterColor = "Red";
                ReturnMessage.RegisterMessage = "Unable to Register, Server status: Offline";
            }
        }
        public RelayCommand ClearCommand { get; private set; }
        private void OnClear()
        {
            Password = "";
            PasswordBorder = "Gray";
            UserNameBorder = "Gray";
            ReturnMessage.RegisterColor = "Black";
            ReturnMessage.RegisterMessage = "Register";
        }
    }
}
