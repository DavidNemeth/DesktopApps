using System.ServiceModel;

namespace DchatServices.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IClientServices" in both code and config file together.
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        void GetMessage(string message, string userName);
        [OperationContract]
        void Update(bool value, string username);
    }
}
