using ChattingInterfaces;
using ChattingServer.ServiceModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ChattingServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChattingService : IChattingService
    {
        public ConcurrentDictionary<string, Client> _connectedClients =
            new ConcurrentDictionary<string, Client>();

        ClientContext db = new ClientContext();

        // true->logged in  // false->username already in use
        public bool Login(string userName, string password)
        {
            Client user = db.Clients.FirstOrDefault(p => p.UserName == userName && p.Password == password);
            if (user == null)
            {
                return false;
            }
            else
            {
                var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClientService>();

                user.connection = establishedUserConnection;
                _connectedClients.TryAdd(userName, user);
                updateHelper(true, userName);

                user.LoggedIn = true;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Client login: {0} with id: {1} at {2}", user.UserName, user.UserID, DateTime.Now);
                Console.ResetColor();
                return true;
            }
        }

        public void Logout()
        {
            Client client = GetMyClient();
            if (client != null)
            {
                Client removedClient;
                _connectedClients.TryRemove(client.UserName, out removedClient);

                updateHelper(false, removedClient.UserName);
                client.LoggedIn = false;

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

        public Client GetMyClient()
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

        private void updateHelper(bool value, string userName)
        {
            foreach (var client in _connectedClients)
            {
                if (client.Value.UserName.ToLower() != userName.ToLower())
                {
                    client.Value.connection.Update(value, userName);
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

        public void Register(string userName, string password)
        {
            Client newUser = new Client();
            newUser.UserName = userName;
            newUser.Password = password;
            Save();
        }

        private void Save()
        {
            db.SaveChanges();
        }

    }
}
