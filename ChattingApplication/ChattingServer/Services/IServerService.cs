using ChatModel;
using ChatModel.Users;
using System.Collections.Generic;
using System.ServiceModel;

namespace ChattingServer.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServerService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IServerService))]
    public interface IServerService
    {
        void Save();
        void Register(string userName, string password);
        [OperationContract]
        bool Login(string userName, string password);
        [OperationContract]
        void SendMessageToAll(string message, string userName);
        [OperationContract]
        void Logout();
        [OperationContract]
        List<ProfileModel> GetCurrentUsers();
    }
}
