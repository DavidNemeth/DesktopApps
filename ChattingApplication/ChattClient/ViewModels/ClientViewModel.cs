using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Login = new RelayCommand(OnLogin);
            Logout = new RelayCommand(OnLogout);
            Send = new RelayCommand(OnSend);
            Users = Server.GetCurrentUsers();
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
            set { SetProperty(ref message, value); }
        }


        public List<string> Users { get; set; }

        private string chat;
        public string Chat
        {
            get { return chat; }
            set { SetProperty(ref chat, value); }
        }
        
        #endregion

        public RelayCommand Login { get; private set; }
        private void OnLogin()
        {
            //UserList.Add(UserName);
            int returnValue = Server.Login(UserName);
            if (returnValue == 0)
            {
                LoadUserList(Server.GetCurrentUsers());
            }
        }

        public RelayCommand Logout { get; private set; }
        private void OnLogout()
        {
            // Users.Remove(userName);
        }

        public RelayCommand Send { get; private set; }
        private void OnSend()
        {
            if (message == "")
            {
                return;
            }
            else
            {
                Server.SendMessageToAll(message, userName);
                TakeMessage(message, "You");
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

        private void LoadUserList(List<string> users)
        {
            foreach (var user in users)
            {
                AddUserToList(user);
            }
        }
        public void AddUserToList(string userName)
        {
            if (Users.Contains(userName))
            {
                return;
            }
            else
            {
                Users.Add(userName);
            }
        }
    }
}
