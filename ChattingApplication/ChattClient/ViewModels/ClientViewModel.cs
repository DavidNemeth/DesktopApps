using System.Collections.Generic;
using System.ServiceModel;
using ChattingInterfaces;
using ChattClient.Helpers;
using System.Collections.ObjectModel;
using ChattClient.ClientSideServices;

namespace ChattClient.ViewModels
{
    public class ClientViewModel : BindableBase
    {
        private static IChattingService _server;
        public static ClientViewModel This;
        public ClientViewModel()
        {
            var channelFactory = new DuplexChannelFactory<IChattingService>(new ClientService(), "ChattingServiceEndPoint");
            _server = channelFactory.CreateChannel();
            This = this;
            CreateCommands();
        }
        private void CreateCommands()
        {
            Login = new RelayCommand(OnLogin);
            Logout = new RelayCommand(OnLogout);
            Send = new RelayCommand(OnSend);
            Register = new RelayCommand(OnRegister);
        }
        public RelayCommand Register { get; private set; }
        public void OnRegister()
        {
            
            _server.Register(UserName, Pw);
        }
        #region props
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                SetProperty(ref _message, value);
                OnPropertyChanged("SendEnabled");
            }
        }

        private string _chat;
        public string Chat
        {
            get { return _chat; }
            set { SetProperty(ref _chat, value); }
        }

        private ObservableCollection<string> _users = new ObservableCollection<string>();
        public ObservableCollection<string> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }
        public string Pw { get; set; }
        #endregion
        #region converterProps
        private string _bordercolor = "Gray";
        public string BorderColor
        {
            get { return _bordercolor; }
            set
            {
                SetProperty(ref _bordercolor, value);
            }
        }
        private bool _loginvis = true;
        public bool LoginVis
        {
            get { return _loginvis; }
            set
            {
                SetProperty(ref _loginvis, value);
            }
        }
        private bool _logoutvis;
        public bool LogoutVis
        {
            get { return _logoutvis; }
            set
            {
                SetProperty(ref _logoutvis, value);
                OnPropertyChanged("SendEnabled");
            }
        }
        #endregion

        public RelayCommand Login { get; private set; }
        public void OnLogin()
        {            
            if (_server.Login(UserName, Pw))
            {
                LoadUserList(_server.GetCurrentUsers());
                LogoutVis = true;
                LoginVis = false;
                BorderColor = "Green";
            }
        }

        public RelayCommand Logout { get; private set; }
        private void OnLogout()
        {
            _server.Logout();
            Users.Clear();
            UserName = "";
            LoginVis = true;
            LogoutVis = false;
            BorderColor = "Gray";
        }

        public RelayCommand Send { get; private set; }
        private void OnSend()
        {
            if (string.IsNullOrEmpty(Message))
            {
                return;
            }
            _server.SendMessageToAll(Message, UserName);
            TakeMessage(Message, "You");
            Message = "";
        }

        public void TakeMessage(string message, string username)
        {
            Chat += username + ": " + message + "\n";
        }

        public static ClientViewModel GetInstance()
        {
            return This;
        }

        private void LoadUserList(List<string> users)
        {
            foreach (var user in users)
            {
                if (Users.Contains(user))
                {
                    return;
                }
                else
                {
                    Users.Add(user);
                }
            }
        }
    }
}
