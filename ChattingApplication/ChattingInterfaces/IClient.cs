using System.ServiceModel;

namespace ChattingInterfaces
{
    public interface IClient
    {
        [OperationContract]
        void GetMessage(string message, string userName);
    }
}
