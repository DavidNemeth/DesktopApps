using System;
using System.ServiceModel;
using ChattingServer.ServerSideServices;
using ChattingServer.ServiceModel;

namespace ChattingServer
{
    class Program
    {
        public static ChattingService Server;
        static void Main()
        {
            Server = new ChattingService();
            ServiceHost host = new ServiceHost(Server);
            {
                host.Open();
                Console.WriteLine("<Server is Open>");
                for (;;)
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
                                Server.WipeUsers();
                            break;
                        case "ban":
                            Console.Write("User to ban: ");
                            string usertoban = Console.ReadLine().ToLower();
                            if (Server.ConnectedClients.Keys.Contains(usertoban))
                            {
                                Server.LogoutUser(usertoban);
                            }
                            Console.WriteLine(Server.BanUser(usertoban) ? "User banned" : "Invalid username");
                            break;
                        case "userinfo":
                            Console.Write("User: ");
                            string username = Console.ReadLine().ToLower();
                            Client user = Server.GetUserByName(username);
                            if (user != null)
                            {
                                Console.WriteLine("User id: {0}", user.UserId);
                                Console.WriteLine("UserName: {0}", user.UserName);
                                Console.WriteLine("Password: {0}", user.Password);
                                Console.WriteLine("Logged In: {0}", user.LoggedIn);
                            }
                            break;
                        case "rename":
                            Console.Write("User to rename: ");
                            string userToRename = Console.ReadLine().ToLower();
                            if (Server.GetUserByName(userToRename) != null)
                            {
                                Console.Write("Enter new name: ");
                                string newname = Console.ReadLine();
                                Console.WriteLine(Server.Rename(userToRename, newname)
                                    ? "User renamed"
                                    : "Invalid new name");
                            }
                            else
                                Console.WriteLine("User not found");
                            break;
                        case "users":
                            foreach (var item in Server.GetUserNames())
                            {
                                Console.WriteLine("-" + item);
                            }
                            break;

                        case "open":
                            if (host.State.ToString() != "Opened")
                            {
                                host = new ServiceHost(Server);
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
                            Console.WriteLine(host.State.ToString() != "Opened"
                                ? "<Server is Closed>"
                                : "<Server is Open>");

                            break;
                    }
                }
            }
        }
    }
}
