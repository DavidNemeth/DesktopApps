using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows.Controls;

namespace ModernChattingClient.Pages.Home
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private static Login _this;
        public Login()
        {
            InitializeComponent();            
        }

        public static Login GetInstance()
        {
            return _this;
        }
    }
}
