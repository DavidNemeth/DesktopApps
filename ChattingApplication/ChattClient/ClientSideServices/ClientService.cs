using ChattingInterfaces;
using System.ServiceModel;
using System;
using System.Windows;
using ChattClient.Views;
using System.Windows.Controls;

namespace ChattClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]    
    public class ClientService : IClientService
    {        
        public void GetMessage(string message, string userName)
        {            
            TakeMessage(message, userName);
            currentview.TakeMessage(message, userName);
        }

        public void GetUpdate(int value, string userName)
        {

            switch (value)
            {
                case 0:
                    {
                        currentview.AddUserToList(userName);
                        //((MainWindow)Application.Current.MainWindow).AddUserToList(userName);                        
                        break;
                    }
                case 1:
                    {
                        currentview.RemoveUserFromList(userName);
                        //((ClientView)Application.Current.ClientView).RemoveUserFromList(userName);
                        break;
                    }
            }
        }
    }
}
