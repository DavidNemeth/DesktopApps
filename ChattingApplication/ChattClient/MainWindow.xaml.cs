using ChattingInterfaces;
using System.ServiceModel;
using System.Windows;

namespace ChattClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IChattingService Server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;
        public MainWindow()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientCallback(), "ChattingServiceEndPoint");
            Server = _channelFactory.CreateChannel();
        }
        public void TakeMessage(string message, string userName)
        {
            TextAreaTxtBox.Text += userName + ": " + message + "\n";
            TextAreaTxtBox.ScrollToEnd();
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            Server.SendMessageToAll(MessageAreaTxtBox.Text, userNameTxtBx.Text);
            TakeMessage(MessageAreaTxtBox.Text, "You");
            MessageAreaTxtBox.Text = "";
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            int returnValue = Server.Login(userNameTxtBx.Text);
            if (returnValue == 1)
            {
                MessageBox.Show("That name is already in use!");
            }
            else if (returnValue == 0)
            {
                WelcomeLbl.Content = "Welcome, " + userNameTxtBx.Text;
                userNameTxtBx.IsEnabled = false;
                LoginBtn.IsEnabled = false;
            }
        }
    }
}
