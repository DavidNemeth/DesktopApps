using ModernChattingClient.Base;

namespace ModernChattingClient.Pages.Home
{
    public class ReturnMessages : BindableBase
    {

        #region REGISTER
        private string _registermessage = "Register";
        public string RegisterMessage
        {
            get
            {
                return _registermessage;
            }
            set
            {
                SetProperty(ref _registermessage, value);
            }
        }

        private string _registercolor = "Black";
        public string RegisterColor
        {
            get
            {
                return _registercolor;
            }
            set
            {
                SetProperty(ref _registercolor, value);
            }
        }
        #endregion
        #region LOGIN
        private string _loginmessage = "Login";
        public string LoginMessage
        {
            get
            {
                return _loginmessage;
            }
            set
            {
                SetProperty(ref _loginmessage, value);
            }
        }

        private string _logincolor = "Black";
        public string LoginColor
        {
            get
            {
                return _logincolor;
            }
            set
            {
                SetProperty(ref _logincolor, value);
            }
        }
        #endregion
    }
}
