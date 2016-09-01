using DchatClient.ViewModel;
using DchatServices.Services;
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
                ClientViewModel.GetInstance().UserList.Add(userName);
            }
            else
            {
                ClientViewModel.GetInstance().UserList.Remove(userName);
            }
        }
    }
}
