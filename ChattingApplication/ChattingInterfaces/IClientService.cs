using System.ServiceModel;

namespace ChattingInterfaces
{
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        void GetMessage(string message, string userName);       
        [OperationContract]
        void Update(bool value, string userName);

    }
}
