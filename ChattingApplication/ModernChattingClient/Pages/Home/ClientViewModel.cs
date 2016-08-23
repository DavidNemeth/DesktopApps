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
        HomePage instance = HomePage.GetInstance();

        public ClientViewModel()
        {
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientService(), "ChattingServiceEndPoint");
            Server = _channelFactory.CreateChannel();
            _this = this;
            CreateCommands();
            NavigationCommands();
        }
        private void CreateCommands()
        {
            Login = new Base.RelayCommand(OnLogin);
            Logout = new Base.RelayCommand(OnLogout);
            Send = new Base.RelayCommand(OnSend);
            Register = new Base.RelayCommand(OnRegister);
            ClearCommand = new Base.RelayCommand(OnClear);
        }
        private void NavigationCommands()
        {
            ToLogin = new Base.RelayCommand(NavigateToLogin);
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
        private string loginvisibile;
        private string usernameborder = "Gray";
        private string passwordborder = "Gray";
        private string regmessage;
        private ReturnMessages returnmessage = new ReturnMessages();
        private bool chatenabled;
        public bool ChatEnabled
        {
            get { return chatenabled; }
            set
            {
                SetProperty(ref chatenabled, value);
            }
        }
        public string LoginVisible
        {
            get
            {
                return loginvisibile;
            }
            set
            {
                SetProperty(ref loginvisibile, value);
            }
        }
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
        #endregion

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
        public Base.RelayCommand ToLogin { get; private set; }
        public void NavigateToLogin()
        {
            BBCodeBlock bs = new BBCodeBlock();
            bs.LinkNavigator.Navigate(new Uri("/Pages/HomePage.xaml", UriKind.Relative), instance);
        }

        public Base.RelayCommand Login { get; private set; }
        public void OnLogin()
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
            try
            {
                if (Server.Login(UserName, Password))
                {
                    LoadUserList(Server.GetCurrentUsers());
                    LoginVis = false;
                    LogoutVis = true;
                    ChatEnabled = true;
                    Chat = "";
                    BBCodeBlock bs = new BBCodeBlock();
                    bs.LinkNavigator.Navigate(new Uri("/Pages/ChatPage.xaml", UriKind.Relative), instance);
                    ReturnMessage.LoginMessage = "Logged In as: " + UserName;
                    LoginVisible = "hidden";
                    return;
                }
                else
                    UserNameBorder = "Red";
                return;
            }
            catch (Exception)
            {
                ReturnMessage.LoginColor = "Red";
                ReturnMessage.LoginMessage = "Unable to connect, Server status: Offline";
            }
        }

        public Base.RelayCommand Logout { get; private set; }
        private void OnLogout()
        {
            try
            {
                Server.Logout();
            }
            catch (Exception)
            {
            }
            Users.Clear();
            CurrentUsers.Clear();
            Chat = "";
            ReturnMessage.LoginMessage = "Log In";
            LoginVis = true;
            ChatEnabled = false;
            LogoutVis = false;
            LoginVisible = "visible";
        }

        public Base.RelayCommand Send { get; private set; }
        private void OnSend()
        {
            if (string.IsNullOrEmpty(Message))
            {
                return;
            }
            try
            {
                Server.SendMessageToAll(Message, UserName);
                TakeMessage(Message, "You");
                Message = "";
            }
            catch (Exception)
            {
                Users.Clear();
                CurrentUsers.Clear();
                ReturnMessage.LoginMessage = "Log In";
                LoginVis = true;
                LogoutVis = false;
            }
        }

        public Base.RelayCommand ClearCommand { get; private set; }
        private void OnClear()
        {           
            Password = "";
            PasswordBorder = "Gray";
            UserNameBorder = "Gray";
            ReturnMessage.LoginColor = "Black";
            ReturnMessage.RegisterColor = "Black";
            ReturnMessage.RegisterMessage = "Regsiter";
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
                    CurrentUsers.Add(new Link() { DisplayName = user, Source = new Uri("/Pages/Chat/MainRoom.xaml", UriKind.Relative) });
                }
            }
        }
    }
}
