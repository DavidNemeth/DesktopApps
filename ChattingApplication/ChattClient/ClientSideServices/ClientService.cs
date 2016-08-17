using ChattingInterfaces;
using System.ServiceModel;
using System;
using System.Windows;
using ChattClient.Views;
using System.Windows.Controls;
using ChattClient.ViewModels;

namespace ChattClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientService : IClientService
    {
        
        public void GetMessage(string message, string userName)
        {
            ClientViewModel.GetInstance().TakeMessage(message, userName);
        }

        public void GetUpdate(int value, string userName)
        {
            switch (value)
            {
                case 0:
                    {
                        ClientViewModel.GetInstance().Users.Add(userName);
                        //((MainWindow)Application.Current.MainWindow).AddUserToList(userName);                        
                        break;
                    }
                case 1:
                    {
                        ClientViewModel.GetInstance().Users.Remove(userName);
                        //((ClientView)Application.Current.ClientView).RemoveUserFromList(userName);
                        break;
                    }
            }
        }
    }
}
