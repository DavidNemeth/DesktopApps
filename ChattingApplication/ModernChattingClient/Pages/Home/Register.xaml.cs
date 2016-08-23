using System.Windows.Controls;

namespace ModernChattingClient.Pages.Home
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        private ClientViewModel context = ClientViewModel.GetInstance();
        public Register()
        {
            InitializeComponent();
            this.DataContext = context;
        }
    }
}
