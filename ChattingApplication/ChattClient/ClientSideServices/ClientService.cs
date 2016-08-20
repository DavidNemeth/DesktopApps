﻿using ChattingInterfaces;
using System.ServiceModel;
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
        
        public void Update(bool value, string userName)
        {
            if (value)
            {
                ClientViewModel.GetInstance().Users.Add(userName);
            }
            else
            {
                ClientViewModel.GetInstance().Users.Remove(userName);
            }
        }
    }
}
