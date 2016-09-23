using DchatServices.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace DchatServices.Services
{
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        void StartUp();
        [OperationContract]
        HashSet<DmUser> GetConnectedUsers();
        [OperationContract]
        DmUser GetUserByName(string username);
        [OperationContract]
        string Login(string userName, string password);
        [OperationContract]
        void Logout();
        [OperationContract]
        string Register(string username, string password);
        [OperationContract]
        void SendMessageToAll(string message, string userName);
    }
}
