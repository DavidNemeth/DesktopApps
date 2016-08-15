using System.ServiceModel;

namespace ChattingInterfaces
{
    public interface IClient
    {
        [OperationContract]
        void GetMessage(string message, string userName);
        [OperationContract]
        //1->log in 2->log out
        void GetUpdate(int value, string userName);        
    }
}
