using ChattingInterfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChattingServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChattingService : IChattingService
    {
        public ConcurrentDictionary<string, ClientModel> _connectedClients =
            new ConcurrentDictionary<string, ClientModel>();


        // 0->logged in  // 1->username already in use
        public int Login(string userName)
        {
            if (userName == "")
            {
                return 1;
            }
            foreach (var client in _connectedClients)
            {
                if (client.Key.ToLower() == userName.ToLower())
                {
                    return 1;
                }
            }
            if (userName == null)
            {
                return 1;
            }
            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClientService>();

            ClientModel newClient = new ClientModel();
            newClient.connection = establishedUserConnection;
            newClient.UserName = userName;

            updateHelper(0, userName);

            _connectedClients.TryAdd(userName, newClient);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Client login: {0} at {1}", newClient.UserName, DateTime.Now);
            Console.ResetColor();

            return 0;
        }

        public void Logout()
        {
            ClientModel client = GetMyClient();
            if (client != null)
            {
                ClientModel removedClient;
                _connectedClients.TryRemove(client.UserName, out removedClient);

                updateHelper(1, removedClient.UserName);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Client logoff: {0} at {1}", removedClient.UserName, DateTime.Now);
                Console.ResetColor();
            }
        }

        public void SendMessageToAll(string message, string userName)
        {
            foreach (var client in _connectedClients)
            {
                if (client.Key.ToLower() != userName.ToLower())
                {
                    client.Value.connection.GetMessage(message, userName);
                }
            }
        }

        public ClientModel GetMyClient()
        {
            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClientService>();

            foreach (var client in _connectedClients)
            {
                if (client.Value.connection == establishedUserConnection)
                {
                    return client.Value;
                }
            }
            return null;
        }

        private void updateHelper(int value, string userName)
        {
            foreach (var client in _connectedClients)
            {
                if (client.Value.UserName.ToLower() != userName.ToLower())
                {
                    client.Value.connection.GetUpdate(value, userName);
                }                
            }
        }

        public List<string> GetCurrentUsers()
        {
            List<string> listOfUsers = new List<string>();
            foreach (var client in _connectedClients)
            {
                listOfUsers.Add(client.Value.UserName);
            }
            return listOfUsers;
        }
    }
}
