using ModernChattingClient.Base;
using System.Collections.ObjectModel;

namespace ModernChattingClient.Pages.Chat
{
    public class ChatViewModel : BindableBase
    {
        public static ChatViewModel _this;

        public ChatViewModel()
        {
            _this = this;
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
        public string Pw { get; set; }
        #endregion


        public static ChatViewModel GetInstance()
        {
            return _this;
        }

        public void TakeMessage(string message, string username)
        {
            Chat += username + ": " + message + "\n";
        }
    }
}
