using ChattingInterfaces;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using ModernChattingClient.Base;
using ModernChattingClient.ClientServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel;

namespace ModernChattingClient.Pages.Home
{
    public class ClientViewModel : BindableBase, IDataErrorInfo
    {
        private static IChattingService _server;
        private static ClientViewModel _this;

        private readonly HomePage _instance = HomePage.GetInstance();

        public ClientViewModel()
        {
            var channelFactory = new DuplexChannelFactory<IChattingService>(new ClientService(), "ChattingServiceEndPoint");
            _server = channelFactory.CreateChannel();
            _this = this;
            CreateCommands();
            NavigationCommands();           
        }
        private void CreateCommands()
        {
            Login = new Base.RelayCommand(OnLogin, () => !(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password)));
            Logout = new Base.RelayCommand(OnLogout);
            Send = new Base.RelayCommand(OnSend);
            ClearCommand = new Base.RelayCommand(OnClear);
        }

        private void NavigationCommands()
        {
            ToLogin = new Base.RelayCommand(NavigateToLogin);
        }
        #region props        
        private string _username;
        private string _password;
        private string _message;
        private string _chat;
        private ObservableCollection<string> _users = new ObservableCollection<string>();
        private LinkCollection _currentusers = new LinkCollection();

        public string UserName
        {
            get { return _username; }
            set
            {
                SetProperty(ref _username, value);
                UserNameBorder = "Gray";
                Login.RaiseCanExecuteChanged();                
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                PasswordBorder = "Gray";
                Login.RaiseCanExecuteChanged();                
            }
        }
        public string Message
        {
            get { return _message; }
            set
            {
                SetProperty(ref _message, value);
            }
        }
        public string Chat
        {
            get { return _chat; }
            set { SetProperty(ref _chat, value); }
        }

        public ObservableCollection<string> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }
        public LinkCollection CurrentUsers
        {
            get { return _currentusers; }
            set { SetProperty(ref _currentusers, value); }
        }
        #endregion
        #region converterProps    
        private bool _loginvis = true;
        private bool _logoutvis;
        private string _loginvisibile;
        private string _usernameborder = "Gray";
        private string _passwordborder = "Gray";
        private ReturnMessages _returnmessage = new ReturnMessages();
        private bool _chatenabled;
        public bool ChatEnabled
        {
            get { return _chatenabled; }
            set
            {
                SetProperty(ref _chatenabled, value);
            }
        }
        public string CurrentUser { get; set; }
        public string LoginVisible
        {
            get
            {
                return _loginvisibile;
            }
            set
            {
                SetProperty(ref _loginvisibile, value);
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

        public bool LoginVis
        {
            get { return _loginvis; }
            set
            {
                SetProperty(ref _loginvis, value);
            }
        }
        public bool LogoutVis
        {
            get { return _logoutvis; }
            set
            {
                SetProperty(ref _logoutvis, value);
                OnPropertyChanged("SendEnabled");
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
        #endregion


        public Base.RelayCommand ToLogin { get; private set; }
        public void NavigateToLogin()
        {
            BBCodeBlock bs = new BBCodeBlock();
            bs.LinkNavigator.Navigate(new Uri("/Pages/HomePage.xaml", UriKind.Relative), _instance);
        }
        public Base.RelayCommand Login { get; private set; }
        public void OnLogin()
        {
            try
            {
                if (_server.Login(UserName, Password))
                {
                    LoadUserList(_server.GetCurrentUsers());
                    LoginVis = false;
                    LogoutVis = true;
                    ChatEnabled = true;
                    Chat = "";
                    BBCodeBlock bs = new BBCodeBlock();
                    bs.LinkNavigator.Navigate(new Uri("/Pages/ChatPage.xaml", UriKind.Relative), HomePage.GetInstance(), NavigationHelper.FrameSelf);
                    CurrentUser = UserName;
                    ReturnMessage.LoginMessage = "Logged In as: " + UserName;
                }
                else
                {
                    ReturnMessage.LoginColor = "Red";
                    ReturnMessage.LoginMessage = "Incorrect Username or Password";
                }
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
                _server.Logout();
            }
            catch (Exception)
            {
                // ignored
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
                _server.SendMessageToAll(Message, UserName);
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
        }
        public void TakeMessage(string message, string username)
        {
            Chat += username + ": " + message + "\n";
        }

        public static ClientViewModel GetInstance()
        {
            return _this;
        }

        private void LoadUserList(IEnumerable<string> userlist)
        {
            CurrentUsers = new LinkCollection();
            foreach (var user in userlist)
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

        #region errors
        public string Error => null;

        public string this[string columnName] => null;

        #endregion
    }
}
