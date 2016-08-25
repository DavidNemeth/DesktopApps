using System.Windows.Controls;

namespace ModernChattingClient.Pages.Home
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {       
        public Register()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }
    }
}
