using ChattingInterfaces;
using System.ServiceModel;
using System;
using System.Windows;

namespace ChattClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientCallback : IClient
    {
        public void GetMessage(string message, string userName)
        {
            ((MainWindow)Application.Current.MainWindow).TakeMessage(message, userName);
        }

        public void GetUpdate(int value, string userName)
        {
            switch (value)
            {
                case 0:
                    {
                        ((MainWindow)Application.Current.MainWindow).AddUserToList(userName);
                        break;
                    }
                case 1:
                    {
                        ((MainWindow)Application.Current.MainWindow).RemoveUserFromList(userName);
                        break;
                    }
            }
        }
    }
}
