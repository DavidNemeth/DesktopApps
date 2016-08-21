using System.Windows.Controls;

namespace ModernChattingClient.Pages.Home
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            this.DataContext = new ClientViewModel();
        }
    }
}
