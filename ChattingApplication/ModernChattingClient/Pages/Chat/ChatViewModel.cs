using FirstFloor.ModernUI.Presentation;
using ModernChattingClient.Base;
using ModernChattingClient.Pages.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ModernChattingClient.Pages.Chat
{
    public class ChatViewModel : BindableBase
    {
        public static ChatViewModel _this;

        public ChatViewModel()
        {
            _this = this;
            CurrentUsers = new LinkCollection();
            foreach (var user in UserNames)
            {
                CurrentUsers.Add(new Link() { DisplayName = user });
            }
        }

        #region props
        public LinkCollection CurrentUsers { get; private set; }

        private ObservableCollection<string> usernames = new ObservableCollection<string>();
        public ObservableCollection<string> UserNames
        {
            get { return usernames; }
            set { SetProperty(ref usernames, value); }
        }

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
        #endregion


        public static ChatViewModel GetInstance()
        {
            return _this;
        }

        public void TakeMessage(string message, string username)
        {
            Chat += username + ": " + message + "\n";
        }

        public void LoadUserList(List<string> users)
        {
            foreach (var user in users)
            {
                if (UserNames.Contains(user))
                {
                    return;
                }
                else
                {
                    CurrentUsers.Add(new Link() { DisplayName = user });
                }
            }
        }
    }
}
