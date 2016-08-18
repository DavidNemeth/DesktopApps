using ChattClient.ViewModels;
using System.Windows.Controls;

namespace ChattClient.Views
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : UserControl
    {
        //public static IChattingService Server;
        //private static DuplexChannelFactory<IChattingService> _channelFactory;
        public static ClientView _this;
        public ClientView()
        {
            this.DataContext = new ClientViewModel();
            InitializeComponent();
            _this = this;
            //_channelFactory = new DuplexChannelFactory<IChattingService>(new ClientService(), "ChattingServiceEndPoint");
            //Server = _channelFactory.CreateChannel();
        }

        public static ClientView GetInstance()
        {
            return _this;
        }

        private void userNameTxtBx_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        //public void TakeMessage(string message, string userName)
        //{
        //    TextAreaTxtBox.Text += userName + ": " + message + "\n";
        //    TextAreaTxtBox.ScrollToEnd();
        //}

        //private void SendBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    if (MessageAreaTxtBox.Text == "")
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        Server.SendMessageToAll(MessageAreaTxtBox.Text, userNameTxtBx.Text);
        //        TakeMessage(MessageAreaTxtBox.Text, "You");
        //        MessageAreaTxtBox.Text = "";
        //    }
        //}

        //private void LoginBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    int returnValue = Server.Login(userNameTxtBx.Text);
        //    if (userNameTxtBx.Text == "")
        //    {
        //        MessageBox.Show("Please enter a username!");
        //    }
        //    else if (returnValue == 1)
        //    {
        //        MessageBox.Show("That name is already in use!");
        //    }
        //    else if (returnValue == 0)
        //    {
        //        WelcomeLbl.Content = "Welcome, " + userNameTxtBx.Text;
        //        userNameTxtBx.IsEnabled = false;
        //        LoginBtn.IsEnabled = false;
        //        LogoutBtn.Visibility = Visibility.Visible;
        //        LoginBtn.Visibility = Visibility.Collapsed;
        //        SendBtn.IsEnabled = true;
        //        LogoutBtn.IsEnabled = true;

        //        LoadUserList(Server.GetCurrentUsers());
        //    }
        //}

        //private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    Server.Logout();
        //    LogoutBtn.IsEnabled = false;
        //    LogoutBtn.Visibility = Visibility.Collapsed;
        //    LoginBtn.IsEnabled = true;
        //    LoginBtn.Visibility = Visibility.Visible;
        //    SendBtn.IsEnabled = false;
        //    userNameTxtBx.Text = "";
        //    userNameTxtBx.IsEnabled = true;
        //    UsersListBox.Items.Clear();
        //}        

        //public void AddUserToList(string userName)
        //{
        //    if (UsersListBox.Items.Contains(userName))
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        UsersListBox.Items.Add(userName);
        //    }
        //}

        //public void RemoveUserFromList(string userName)
        //{
        //    if (UsersListBox.Items.Contains(userName))
        //    {
        //        UsersListBox.Items.Remove(userName);
        //    }
        //}

        //private void LoadUserList(List<string> users)
        //{
        //    foreach (var user in users)
        //    {
        //        AddUserToList(user);
        //    }
        //}
    }
}
