using FirstFloor.ModernUI.Windows.Controls;
using ModernChattingClient.Pages.Home;

namespace ModernChattingClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private static MainWindow _this;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ClientViewModel.GetInstance();
            _this = this;
        }

        public static MainWindow GetInstance()
        {
            return _this;
        }
    }
}
