using ModernChattingClient.Pages.Home;
using System.Windows.Controls;

namespace ModernChattingClient.Pages
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : UserControl
    {
        private static ClientViewModel context = ClientViewModel.GetInstance();
        public ChatPage()
        {
            DataContext = context;
            InitializeComponent();            
        }
    }
}
