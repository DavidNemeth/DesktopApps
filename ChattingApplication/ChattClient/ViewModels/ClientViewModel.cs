using System.Collections.Generic;
using System.ServiceModel;
using ChattingInterfaces;
using ChattClient.Helpers;
using System.Collections.ObjectModel;

namespace ChattClient.ViewModels
{
    public class ClientViewModel : BindableBase
    {
        private static IChattingService Server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;
        public static ClientViewModel _this;
        public ClientViewModel()
        {
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientService(), "ChattingServiceEndPoint");
            Server = _channelFactory.CreateChannel();
            _this = this;
            CreateCommands();
        }
        private void CreateCommands()
        {
            Login = new RelayCommand(OnLogin);
            Logout = new RelayCommand(OnLogout);
            Send = new RelayCommand(OnSend);
        }
        #region props
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                SetProperty(ref message, value);
                OnPropertyChanged("SendEnabled");
            }
        }

        private string chat;
        public string Chat
        {
            get { return chat; }
            set { SetProperty(ref chat, value); }
        }

        private ObservableCollection<string> users = new ObservableCollection<string>();
        public ObservableCollection<string> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }
        #endregion
        #region converterProps
        private string bordercolor = "Gray";
        public string BorderColor
        {
            get { return bordercolor; }
            set
            {
                SetProperty(ref bordercolor, value);
            }
        }
        private bool loginvis = true;
        public bool LoginVis
        {
            get { return loginvis; }
            set
            {
                SetProperty(ref loginvis, value);
            }
        }
        private bool logoutvis;
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


        private void CloseWindow()
        {
            //Do your operations
        }
        public RelayCommand Login { get; private set; }
        private void OnLogin()
        {
            //UserList.Add(UserName);
            int returnValue = Server.Login(UserName);
            if (returnValue == 0)
            {
                LoadUserList(Server.GetCurrentUsers());
                LogoutVis = true;
                LoginVis = false;
                BorderColor = "Green";
            }
        }

        public RelayCommand Logout { get; private set; }
        private void OnLogout()
        {
            Server.Logout();
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
