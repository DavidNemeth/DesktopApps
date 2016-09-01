using DchatServices.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace DchatServices.Services
{
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        List<DmUser> GetUsers();
        [OperationContract]
        DmUser GetUserByName(string username);
        [OperationContract]
        int Login(string userName, string password);
        [OperationContract]
        void Logout();
        [OperationContract]
        int Register(string username, string password);
        [OperationContract]
        void SendMessageToAll(string message, string userName);
    }   
}
