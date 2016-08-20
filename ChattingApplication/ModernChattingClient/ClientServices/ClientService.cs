using ChattingInterfaces;
using ModernChattingClient.Pages.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ModernChattingClient.ClientServices
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientService : IClientService
    {
        public void GetMessage(string message, string userName)
        {
            ChatViewModel.GetInstance().TakeMessage(message, userName);
        }

        public void Update(bool value, string userName)
        {
            if (value)
            {
                ChatViewModel.GetInstance().Users.Add(userName);
            }
            else
            {
                ChatViewModel.GetInstance().Users.Remove(userName);
            }
        }
    }
}