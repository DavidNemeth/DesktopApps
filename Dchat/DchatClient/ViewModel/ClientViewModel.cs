using DchatClient.DchatServiceReference;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace DchatClient.ViewModel
{
    public class ClientViewModel : MainViewModel
    {
        private static ClientViewModel _this;
        private static IChatService _server;

        public ClientViewModel()
        {
            ChannelFactory<IChatService> channelFactory = new ChannelFactory<IChatService>("BasicHttpBinding_IChatService");
            _server = channelFactory.CreateChannel();
            _this = this;
            CreateCommands();
        }

        private void CreateCommands()
        {
            Login = new RelayCommand(OnLogin, () => !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)));
            Logout = new RelayCommand(OnLogout);
            //Send = new Base.RelayCommand(OnSend);
            //ClearCommand = new Base.RelayCommand(OnClear);
        }

        #region props  
        private string _password;
        public string Password
        {
            get { return _password; }
            set { Set(() => Password, ref _password, value); }
        }

        private DmUser _user;
        public DmUser User
        {
            get { return _user; }
            set { Set(() => User, ref _user, value); }
        }

        private string _validationmessage;
        public string ValidationMessage
        {
            get { return _validationmessage; }
            set { Set(() => ValidationMessage, ref _validationmessage, value); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { Set(() => Username, ref _username, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { Set(() => Message, ref _message, value); }

        }

        private ObservableCollection<DmUser> _users = new ObservableCollection<DmUser>();
        public ObservableCollection<DmUser> Users
        {
            get { return _users; }
            set { Set(() => Users, ref _users, value); }
        }

        private ObservableCollection<string> _userlist = new ObservableCollection<string>();
        public ObservableCollection<string> UserList
        {
            get { return _userlist; }
            set { Set(() => UserList, ref _userlist, value); }
        }

        private string _chat;
        public string Chat
        {
            get { return _chat; }
            set { Set(() => Chat, ref _chat, value); }

        }
        #endregion

        public void TakeMessage(string message, string username)
        {
            Chat += username + ": " + message + "\n";
        }


        public static ClientViewModel GetInstance()
        {
            return _this;
        }

        public RelayCommand Login { get; private set; }
        public void OnLogin()
        {
            ValidationMessage = _server.Login(Username, Password);
            if (ValidationMessage == "Success")
            {
                LoadUserList(_server.GetUsers());
                User = _server.GetUserByName(Username);
            }
        }

        public RelayCommand Logout { get; private set; }
        public void OnLogout()
        {
            _server.Logout();
            UserList.Clear();
        }
        
        private void LoadUserList(IEnumerable<DmUser> currentusers)
        {
            
            foreach (var user in currentusers)
            {
                if (UserList.Contains(user.Username))
                {
                    return;
                }
                else
                {
                    UserList.Add(user.Username);
                    Users.Add(user);
                }
            }
        }
    }
}
