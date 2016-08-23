using ModernChattingClient.Pages.Home;
using System.Windows.Controls;

namespace ModernChattingClient.Pages.Chat
{
    /// <summary>
    /// Interaction logic for MainRoom.xaml
    /// </summary>
    public partial class MainRoom : UserControl
    {
        private static MainRoom _this;
        
        public MainRoom()
        {
            InitializeComponent();
            DataContext = ClientViewModel.GetInstance();
            _this = this;
        }

        public static MainRoom GetInstance()
        {
            return _this;
        }
    }
}
