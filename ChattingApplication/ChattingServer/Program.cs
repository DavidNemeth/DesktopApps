using System;
using System.ServiceModel;

namespace ChattingServer
{
    class Program
    {
        public static ChattingService _server;
        static void Main(string[] args)
        {
            _server = new ChattingService();
            ServiceHost host = new ServiceHost(_server);
            {
                host.Open();
                Console.WriteLine("<Server is Open>");
                for (; true;)
                {
                    string input = Console.ReadLine().ToLower();
                    switch (input)
                    {
                        case "open":
                            if (host.State.ToString() != "Opened")
                            {
                                host = new ServiceHost(_server);
                                host.Open();
                                Console.Clear();
                                Console.WriteLine("<Server is Open>");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("<Server is Open>");
                            }
                            break;
                        case "close":
                            host.Close();
                            Console.Clear();
                            Console.WriteLine("<Server is Closed>");
                            break;
                        case "exit":
                            Environment.Exit(0);
                            break;                        
                        default:
                            break;
                    }
                }
            }
        }
    }
}
