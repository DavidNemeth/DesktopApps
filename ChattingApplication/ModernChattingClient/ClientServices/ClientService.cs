using ChattingInterfaces;
using ModernChattingClient.Pages.Home;
using System.Linq;
using System.ServiceModel;

namespace ModernChattingClient.ClientServices
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
                ClientViewModel.GetInstance().CurrentUsers.Add(new FirstFloor.ModernUI.Presentation.Link() { DisplayName = userName });
            }
            else
            {
                ClientViewModel.GetInstance().Users.Remove(userName);
                var link = ClientViewModel.GetInstance().CurrentUsers.FirstOrDefault(u => u.DisplayName == userName);
                ClientViewModel.GetInstance().CurrentUsers.Remove(link);
            }
        }
    }
}