using DchatClient.ClientServices;
using DchatClient.DchatServiceReference;
using FirstFloor.ModernUI.Presentation;
using Microsoft.Win32;
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
        #region Initialization
        public ClientViewModel()
        {
            var channelFactory = new DuplexChannelFactory<IChatService>(new ClientService(), "IChatEndpoint");
         

            _server = channelFactory.CreateChannel();
            _this = this;
            CreateCommands();
        }
        private void CreateCommands()
        {
            Login = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnLogin, () => !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)));
            Logout = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnLogout);
            Send = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnSend);
            LoadImage = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(OnLoadImage);
        }
        #endregion

        #region props 
        private ReturnMessage _validation = new ReturnMessage();
        public ReturnMessage Validation
        {
            get { return _validation; }
            set { Set(() => Validation, ref _validation, value); }
        }

        private string _profileImage;
        public string ProfileImage
        {
            get { return _profileImage; }
            set { Set(() => ProfileImage, ref _profileImage, value); }
        }

        private string _loginVisibility = "Visible";
        public string LoginVisibility
        {
            get { return _loginVisibility; }
            set { Set(() => LoginVisibility, ref _loginVisibility, value); }
        }

        private string _profileVisibility = "Hidden";
        public string ProfileVisibility
        {
            get { return _profileVisibility; }
            set { Set(() => ProfileVisibility, ref _profileVisibility, value); }
        }
         
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

        private LinkCollection _connectedUsers = new LinkCollection();
        public LinkCollection ConnectedUsers
        {
            get { return _connectedUsers; }
            set { Set(() => ConnectedUsers, ref _connectedUsers, value); }
        }

        private string _chat;
        public string Chat
        {
            get { return _chat; }
            set { Set(() => Chat, ref _chat, value); }

        }
        #endregion

        #region RelayCommands
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand Login { get; private set; }
        public void OnLogin()
        {
            Validation.LoginMessage = _server.Login(Username, Password);
            if (Validation.LoginMessage == "Success")
            {
                LoadUserList(_server.GetConnectedUsers());
                User = _server.GetUserByName(Username);
                LoginVisibility = "Hidden";
                ProfileVisibility = "Visible";
                Username = "";
                Username = _server.GetMyClient().Username;
            }
            else
            {
                Validation.LoginColor = "Red";
            }
        }

        public GalaSoft.MvvmLight.CommandWpf.RelayCommand Logout { get; private set; }
        public void OnLogout()
        {
            _server.Logout();
            ConnectedUsers.Clear();
            Users.Clear();
            Chat = "";
            LoginVisibility = "Visible";
            ProfileVisibility = "Hidden";
        }

        public GalaSoft.MvvmLight.CommandWpf.RelayCommand Send { get; private set; }
        public void OnSend()
        {
            if (string.IsNullOrEmpty(Message))
            {
                return;
            }
            try
            {
                _server.SendMessageToAll(Message, Username);
                TakeMessage(Message, "You");
                Message = "";
            }
            catch (Exception)
            {
                Users.Clear();
                ConnectedUsers.Clear();
                Validation.LoginMessage = "Log In";
                LoginVisibility = "Visible";
                ProfileVisibility = "Hidden";
            }
        }
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand LoadImage { get; private set; }
        public void OnLoadImage()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = (".png");
            open.Filter = "Pictures (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png";

            if (open.ShowDialog() == true)
                ProfileImage = open.FileName;
        }
        #endregion

        #region Other_methods
        private void LoadUserList(IEnumerable<DmUser> currentusers)
        {
            
            foreach (var user in currentusers)
            {
                if (Users.Contains(user))
                {
                    return;
                }
                else
                {
                    Users.Add(user);
                    ConnectedUsers.Add(new Link() { DisplayName = user.Username, Source = new Uri("/Pages/Chat/MainRoom.xaml", UriKind.Relative) });
                }
            }
        }

        public void TakeMessage(string message, string username)
        {
            Chat += username + ": " + message + "\n";
        }


        public static ClientViewModel GetInstance()
        {
            return _this;
        }
        #endregion
    }
}
