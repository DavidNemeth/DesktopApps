using DchatClient.ViewModel;
using FirstFloor.ModernUI.Presentation;
using System.ServiceModel;
using DchatServices.Services;
using System.Linq;

namespace DchatClient.ClientServices
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientService : IClientService
    {
        public void GetMessage(string message, string username)
        {
            ClientViewModel.GetInstance().TakeMessage(message, username);
        }

        public void Update(bool value, string username)
        {
            
            if (value)
            {
                ClientViewModel.GetInstance().ConnectedUsers.Add(new Link() { DisplayName = username });     
            }
            else
            {
                var link = ClientViewModel.GetInstance().ConnectedUsers.FirstOrDefault(u => u.DisplayName == username);
                ClientViewModel.GetInstance().ConnectedUsers.Remove(link);                
            }
        }
    }
}
