using DchatClient.DchatServiceReference;
using DchatClient.ViewModel;
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

        private ReturnMessage _validation = new ReturnMessage();
        public ReturnMessage Validation
        {
            get { return _validation; }
            set { Set(() => Validation, ref _validation, value); }
        }

        private string _loginVisibility = "Visible";
        public string LoginVisibility
        {
            get { return _loginVisibility; }
            set { Set(() => LoginVisibility, ref _loginVisibility, value); }
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
            Validation.LoginMessage = _server.Login(Username, Password);
            if (Validation.LoginMessage == "Success")
            {
                LoadUserList(_server.GetConnectedUsers());
                User = _server.GetUserByName(Username);
                LoginVisibility = "Hidden";
            }
            else
            {
                Validation.LoginColor = "Red";
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
