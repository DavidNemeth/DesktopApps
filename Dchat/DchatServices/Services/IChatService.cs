using DchatServices.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace DchatServices.Services
{
    [ServiceContract(CallbackContract = typeof(IClientService))]
    public interface IChatService
    {
        [OperationContract]
        void StartUp();
        [OperationContract]
        DmUser GetMyClient();
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
        [OperationContract]
        bool UpdateUser(string NewUsername, string OldUsername, byte[] image);
    }
}
