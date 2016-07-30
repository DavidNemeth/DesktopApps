using System;
using System.ServiceModel;

namespace EmployeesConsoleService
{
    class Program
    {
        static void Main(string[] args)
        {
            using(ServiceHost host = new ServiceHost(typeof(WCFemployeesService)))
            {
                host.Open();
                Console.WriteLine("Server is open");
                Console.WriteLine("<Press Enter to close server>");
                Console.ReadLine();
            }
        }
    }
}
