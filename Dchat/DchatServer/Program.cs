using DchatServer.Model;
using DchatServer.Services;
using System.ServiceModel;

namespace DchatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoMapperConfiguration.Configure();
            using (ServiceHost host = new ServiceHost(typeof(ChatService)))
            {
                host.Open();
                System.Console.WriteLine("<Server is Open>");
            }
            for (;;)
            {

            }
        }
    }
}