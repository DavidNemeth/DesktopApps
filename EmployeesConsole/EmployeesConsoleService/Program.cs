using EmployeesConsoleInterfaces;
using System;
using System.ServiceModel;

namespace EmployeesConsoleService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(WCFemployeesService));
            {
                host.Open();
                Console.WriteLine("<Server is open>");
                for (; true;)
                {
                    string input = Console.ReadLine().ToLower();
                    switch (input)
                    {
                        case "open":
                            if (host.State.ToString() != "Opened")
                            {
                                host = new ServiceHost(typeof(WCFemployeesService));
                                host.Open();
                            }
                            Console.WriteLine("<host is open>");
                            break;
                        case "close":
                            host.Close();
                            Console.WriteLine("<host is closed>");
                            break;
                        case "exit":
                            Environment.Exit(0);
                            break;
                        case "info":
                            Console.WriteLine(host.State);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
