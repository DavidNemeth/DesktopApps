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
                        case "help":
                            Console.WriteLine("Server Commands:\n open -open server \n close -close server \n exit -close server application \nDatabase Commands:\n users -list user names\n user+name -display user information");
                            break;

                        case "users":
                            foreach (var item in _server.GetUserNames())
                            {
                                Console.WriteLine("-"+item);
                            }
                            break;
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
