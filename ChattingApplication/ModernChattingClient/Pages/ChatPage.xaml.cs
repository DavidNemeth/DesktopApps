using ModernChattingClient.Pages.Home;
using System.Windows.Controls;

namespace ModernChattingClient.Pages
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : UserControl
    {        
        private static ChatPage _this;
        public ChatPage()
        {           
            InitializeComponent();
            _this = this;           
        }
        public static ChatPage GetInstance()
        {
            return _this;
        }
    }
}
