using DchatClient.ClientServices;
using DchatClient.DchatServiceReference;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System.ServiceModel;

namespace DchatClient.ViewModel
{
    public class RegisterViewModel : MainViewModel
    {
        private static IChatService _server;

        public RegisterViewModel()
        {
            var channelFactory = new DuplexChannelFactory<IChatService>(new ClientService(), "IChatEndpoint");
            _server = channelFactory.CreateChannel();
            CreateCommands();
        }
        private void CreateCommands()
        {
            Register = new RelayCommand(OnRegister, () => !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)));
        }

        private ReturnMessage _validation = new ReturnMessage();
        public ReturnMessage Validation
        {
            get { return _validation; }
            set { Set(() => Validation, ref _validation, value); }
        }        

        private string _username;
        public string Username
        {
            get { return _username; }
            set { Set(() => Username, ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { Set(() => Password, ref _password, value); }
        }
        public RelayCommand Register { get; private set; }
        public void OnRegister()
        {
            Validation.RegisterMessage = _server.Register(Username, Password);
            if (Validation.RegisterMessage == "Registration Complete!")
                Validation.RegisterColor = "Green";
            else
                Validation.RegisterColor = "Red";
        }       
    }
}
