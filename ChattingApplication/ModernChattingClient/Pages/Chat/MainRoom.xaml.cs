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

        private ClientViewModel context = ClientViewModel.GetInstance();
        public MainRoom()
        {
            InitializeComponent();
            //DataContext = context;
            _this = this;
        }

        public static MainRoom GetInstance()
        {
            return _this;
        }
    }
}
