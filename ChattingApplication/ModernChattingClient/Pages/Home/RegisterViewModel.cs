using ChattingInterfaces;
using ModernChattingClient.Base;
using ModernChattingClient.ClientServices;
using System;
using System.ServiceModel;

namespace ModernChattingClient.Pages.Home
{
    public class RegisterViewModel : BindableBase
    {
        private static IChattingService Server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;
        public RegisterViewModel()
        {
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientService(), "ChattingServiceEndPoint");
            Server = _channelFactory.CreateChannel();
            Register = new Base.RelayCommand(OnRegister);
             ClearCommand = new Base.RelayCommand(OnClear);
        }

        private string username;
        private string password;

        public string UserName
        {
            get { return username; }
            set
            {
                SetProperty(ref username, value);
                UserNameBorder = "Gray";
                OnPropertyChanged("UserName");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                SetProperty(ref password, value);
                PasswordBorder = "Gray";
                OnPropertyChanged("Password");
            }
        }

        private string usernameborder = "Gray";
        private string passwordborder = "Gray";
        private ReturnMessages returnmessage = new ReturnMessages();
        private string regmessage;

        public string RegMessage
        {
            get
            {
                return regmessage;
            }
            set
            {
                SetProperty(ref regmessage, value);
            }
        }

        public string UserNameBorder
        {
            get
            {
                return usernameborder;
            }
            set
            {
                SetProperty(ref usernameborder, value);
            }
        }
        public string PasswordBorder
        {
            get
            {
                return passwordborder;
            }
            set
            {
                SetProperty(ref passwordborder, value);
            }
        }
        public ReturnMessages ReturnMessage
        {
            get
            {
                return returnmessage;
            }
            set
            {
                SetProperty(ref returnmessage, value);
            }
        }

        public Base.RelayCommand Register { get; private set; }
        public void OnRegister()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    UserNameBorder = "Red";
                }
                if (string.IsNullOrEmpty(Password))
                {
                    PasswordBorder = "Red";
                }
                return;
            }
            else
            {
                try
                {
                    if (Server.Register(UserName, Password))
                    {
                        ReturnMessage.RegisterMessage = "Successfuly Registered, Your username: " + UserName;
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
        }
        public Base.RelayCommand ClearCommand { get; private set; }
        private void OnClear()
        {
            Password = "";
            PasswordBorder = "Gray";
            UserNameBorder = "Gray";
            ReturnMessage.RegisterColor = "Black";
            ReturnMessage.RegisterMessage = "Regsiter";
        }
    }
}
