using ModernChattingClient.Base;

namespace ModernChattingClient.Pages.Home
{
    public class ReturnMessages : BindableBase
    {

        #region REGISTER
        private string registermessage = "Register";
        public string RegisterMessage
        {
            get
            {
                return registermessage;
            }
            set
            {
                SetProperty(ref registermessage, value);
            }
        }

        private string registercolor = "Black";
        public string RegisterColor
        {
            get
            {
                return registercolor;
            }
            set
            {
                SetProperty(ref registercolor, value);
            }
        }
        #endregion
        #region LOGIN
        private string loginmessage = "Login";
        public string LoginMessage
        {
            get
            {
                return loginmessage;
            }
            set
            {
                SetProperty(ref loginmessage, value);
            }
        }

        private string logincolor = "Black";
        public string LoginColor
        {
            get
            {
                return logincolor;
            }
            set
            {
                SetProperty(ref logincolor, value);
            }
        }
        #endregion
    }
}
