using ModernChattingClient.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernChattingClient.Pages.Home
{
    public class ReturnMessages : BindableBase
    {
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
    }
}
