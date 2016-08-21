using ChattingInterfaces;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using ModernChattingClient.Base;
using ModernChattingClient.ClientServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace ModernChattingClient.Pages.Home
{
    public class ClientViewModel : BindableBase
    {
        private static IChattingService Server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;
        private static ClientViewModel _this;

        public ClientViewModel()
        {
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientService(), "ChattingServiceEndPoint");
            Server = _channelFactory.CreateChannel();
            _this = this;
            CreateCommands();
        }
        private void CreateCommands()
        {
            Login = new Base.RelayCommand(OnLogin);
            Logout = new Base.RelayCommand(OnLogout);
            Send = new Base.RelayCommand(OnSend);
            Register = new Base.RelayCommand(OnRegister);
        }
        #region props
        private string username;
        private string password;
        private string message;
        private string chat;
        private ObservableCollection<string> users = new ObservableCollection<string>();
        private LinkCollection currentusers = new LinkCollection();

        public string UserName
        {
            get { return username; }
            set
            {
                SetProperty(ref username, value);
                UserNameColor = "Gray";
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
        public string Message
        {
            get { return message; }
            set
            {
                SetProperty(ref message, value);
            }
        }
        public string Chat
        {
            get { return chat; }
            set { SetProperty(ref chat, value); }
        }

        public ObservableCollection<string> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }
        public LinkCollection CurrentUsers
        {
            get { return currentusers; }
            set { SetProperty(ref currentusers, value); }
        }
        #endregion
        #region converterProps        
        private bool loginvis = true;
        private bool logoutvis;
        private string usernamecolor = "Gray";
        private string passwordborder = "Gray";

        public string UserNameColor
        {
            get
            {
                return usernamecolor;
            }
            set
            {
                SetProperty(ref usernamecolor, value);
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
        public bool LoginVis
        {
            get { return loginvis; }
            set
            {
                SetProperty(ref loginvis, value);
            }
        }
        public bool LogoutVis
        {
            get { return logoutvis; }
            set
            {
                SetProperty(ref logoutvis, value);
                OnPropertyChanged("SendEnabled");
            }
        }
        #endregion

        public Base.RelayCommand Register { get; private set; }
        public void OnRegister()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    UserNameColor = "Red";
                }
                if (string.IsNullOrEmpty(Password))
                {
                    PasswordBorder = "Red";
                }
                return;
            }
            else
                Server.Register(UserName, Password);
            UserNameColor = "Green";
        }

        public Base.RelayCommand Login { get; private set; }
        public void OnLogin()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    UserNameColor = "Red";
                }
                if (string.IsNullOrEmpty(Password))
                {
                    PasswordBorder = "Red";
                }
                return;
            }
            if (Server.Login(UserName, Password))
            {
                LoadUserList(Server.GetCurrentUsers());
                BBCodeBlock bs = new BBCodeBlock();
                HomePage instance = HomePage.GetInstance();
                bs.LinkNavigator.Navigate(new Uri("/Pages/ChatPage.xaml", UriKind.Relative), instance);
                return;
            }
            else
                UserNameColor = "Red";
            return;
        }

        public Base.RelayCommand Logout { get; private set; }
        private void OnLogout()
        {
            Server.Logout();
            Users.Clear();
            CurrentUsers.Clear();
        }

        public Base.RelayCommand Send { get; private set; }
        private void OnSend()
        {
            if (string.IsNullOrEmpty(Message))
            {
                return;
            }
            Server.SendMessageToAll(Message, UserName);
            TakeMessage(Message, "You");
            Message = "";
        }

        public void TakeMessage(string message, string username)
        {
            Chat += username + ": " + message + "\n";
        }

        public static ClientViewModel GetInstance()
        {
            return _this;
        }

        private void LoadUserList(List<string> users)
        {
            CurrentUsers = new LinkCollection();
            foreach (var user in users)
            {
                if (Users.Contains(user))
                {
                    return;
                }
                else
                {
                    Users.Add(user);
                    var instance = Pages.Chat.MainRoom.GetInstance();
                    CurrentUsers.Add(new Link() { DisplayName = user });
                }
            }
        }

    }
}
