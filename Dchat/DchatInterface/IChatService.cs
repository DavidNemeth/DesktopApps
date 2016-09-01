using System.ServiceModel;

namespace DchatServer.DchatInterface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChatService" in both code and config file together.
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        int Login(string userName, string password);
    }
}
