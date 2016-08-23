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
                            Console.WriteLine("Server Commands:\n open -open server");
                            Console.WriteLine(" close -close server");
                            Console.WriteLine(" exit -exit server application");
                            Console.WriteLine(" clear -clear console");
                            Console.WriteLine("Database Commands:");
                            Console.WriteLine(" users -list user names");
                            Console.WriteLine(" userinfo -display user information");
                            Console.WriteLine(" rename -rename user");
                            Console.WriteLine(" ban -delete user");
                            Console.WriteLine(" wipe -delete all users from database");
                            Console.WriteLine();
                            break;

                        case "wipe":
                            Console.WriteLine("This action will wipe every database entry, type 'I AGREE' if you wish to proceed");
                            string prompt = Console.ReadLine();
                            if (prompt == "I AGREE")
                                _server.WipeUsers();
                            break;
                        case "ban":
                            Console.Write("User to ban: ");
                            string usertoban = Console.ReadLine().ToLower();
                            if (_server._connectedClients.Keys.Contains(usertoban))
                            {
                                _server.LogoutUser(usertoban);
                            }                           
                            if (_server.BanUser(usertoban))
                            {                                
                                Console.WriteLine("User banned");
                            }                                
                            else
                                Console.WriteLine("Invalid username");
                            break;
                        case "userinfo":
                            Console.Write("User: ");
                            string username = Console.ReadLine().ToLower();
                            Client user = _server.GetUserByName(username);
                            if (user != null)
                            {
                                Console.WriteLine("User id: {0}", user.UserID);
                                Console.WriteLine("UserName: {0}", user.UserName);
                                Console.WriteLine("Password: {0}", user.Password);
                                Console.WriteLine("Logged In: {0}", user.LoggedIn);
                            }
                            break;
                        case "rename":
                            Console.Write("User to rename: ");
                            string userToRename = Console.ReadLine().ToLower();
                            if (_server.GetUserByName(userToRename) != null)
                            {
                                Console.Write("Enter new name: ");
                                string newname = Console.ReadLine();
                                if (_server.Rename(userToRename, newname))
                                    Console.WriteLine("User renamed");
                                else
                                    Console.WriteLine("Invalid new name");
                            }
                            else
                                Console.WriteLine("User not found");
                            break;
                        case "users":
                            foreach (var item in _server.GetUserNames())
                            {
                                Console.WriteLine("-" + item);
                            }
                            break;

                        case "open":
                            if (host.State.ToString() != "Opened")
                            {
                                host = new ServiceHost(_server);
                                host.Open();
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
                            Console.WriteLine("<Server is Closed>");
                            break;

                        case "exit":
                            Environment.Exit(0);
                            break;
                        case "clear":
                            Console.Clear();
                            if (host.State.ToString() != "Opened")
                                Console.WriteLine("<Server is Closed>");
                            else
                                Console.WriteLine("<Server is Open>");

                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
