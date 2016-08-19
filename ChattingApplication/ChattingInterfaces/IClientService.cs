using System.ServiceModel;

namespace ChattingInterfaces
{
    public interface IClientService
    {
        [OperationContract]
        void GetMessage(string message, string userName);
        [OperationContract]
        //1->log in 2->log out
        void GetUpdate(int value, string userName);
        [OperationContract]
        void Update(bool value, string userName);   
    }
}
