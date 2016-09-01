using DchatClient.DchatServiceReference;
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
            ChannelFactory<IChatService> channelFactory = new ChannelFactory<IChatService>("DchatEndpoint");
            _server = channelFactory.CreateChannel();
            _server = channelFactory.CreateChannel();            
            _this = this;
        }
        
        #region props  
        public string Password { get; private set; }        

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

        private void LoadUserList(List<string> currentusers)
        {
            foreach (var user in currentusers)
            {
                if (UserList.Contains(user))
                {
                    return;
                }
                else
                {                                          
                    UserList.Add(user);
                }                
            }
        }
    }
}
