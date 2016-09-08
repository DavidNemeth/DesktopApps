using DchatServices.Model;
using DchatServices.Services;
using System;
using System.ServiceModel;

namespace DchatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ChatService)))
            {
                //host.Open();
                Console.WriteLine("<Server is Open>");
            }
            Console.ReadLine();
        }
    }
}