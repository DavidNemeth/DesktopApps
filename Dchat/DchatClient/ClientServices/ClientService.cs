using DchatClient.ViewModel;
using DchatServices.Services;
using FirstFloor.ModernUI.Presentation;
using System.ServiceModel;

namespace DchatClient.ClientServices
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
                ClientViewModel.GetInstance().ConnectedUsers.Add(new Link() { DisplayName = userName });
            }
            else
            {
                ClientViewModel.GetInstance().ConnectedUsers.Remove(new Link() { DisplayName = userName });
            }
        }
    }
}
