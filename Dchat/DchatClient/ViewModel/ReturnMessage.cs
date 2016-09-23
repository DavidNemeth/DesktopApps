using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DchatClient.ViewModel
{
    public class ReturnMessage : MainViewModel
    {

        #region REGISTER
        private string _registermessage = "Register";
        public string RegisterMessage
        {
            get { return _registermessage; }
            set { Set(() => RegisterMessage, ref _registermessage, value); }
        }


        private string _registercolor;
        public string RegisterColor
        {
            get
            {
                return _registercolor;
            }
            set
            {
                Set(() => RegisterColor, ref _registercolor, value);
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
                Set(() => LoginMessage, ref _loginmessage, value);
            }
        }

        private string _logincolor;
        public string LoginColor
        {
            get
            {
                return _logincolor;
            }
            set
            {
                Set(() => LoginColor, ref _logincolor, value);
            }
        }
        #endregion
    }
}
