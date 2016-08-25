using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ChattingInterfaces;
using ChattingServer.ServiceModel;

namespace ChattingServer.ServerSideServices
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChattingService : IChattingService
    {
        public ConcurrentDictionary<string, Client> ConnectedClients =
            new ConcurrentDictionary<string, Client>();

        private readonly ClientContext _db = new ClientContext();

        // true->logged in  // false->username already in use
        public bool Login(string userName, string password)
        {
            try
            {
                Client user = _db.Clients.FirstOrDefault(p => p.UserName == userName && p.Password == password);
                if (user == null || ConnectedClients.Values.FirstOrDefault(u => u.UserName == userName) != null)
                {
                    return false;
                }
                var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClientService>();

                user.Connection = establishedUserConnection;
                ConnectedClients.TryAdd(userName, user);
                UpdateHelper(true, userName);

                user.LoggedIn = true;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Client login: {0} with id: {1} at {2}", user.UserName, user.UserId, DateTime.Now);
                Console.ResetColor();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Logout()
        {
            Client client = GetMyClient();
            if (client == null) return;
            Client removedClient;
            ConnectedClients.TryRemove(client.UserName, out removedClient);

            UpdateHelper(false, removedClient.UserName);
            client.LoggedIn = false;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Client log-off: {removedClient.UserName} at {DateTime.Now}");
            Console.ResetColor();
        }

        public void SendMessageToAll(string message, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return;
            }
            foreach (var client in ConnectedClients)
            {
                if (client.Key.ToLower() != userName.ToLower())
                {
                    client.Value.Connection.GetMessage(message, userName);
                }
            }
        }

        public string GetUserName()
        {
            try
            {
                return GetMyClient().UserName;
            }
            catch (Exception)
            {

                return "Offline";
            }            
        }

        public Client GetMyClient()
        {
            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClientService>();

            return (from client in ConnectedClients where client.Value.Connection == establishedUserConnection select client.Value).FirstOrDefault();
        }

        private void UpdateHelper(bool value, string userName)
        {
            foreach (var client in ConnectedClients)
            {
                if (!string.Equals(client.Value.UserName, userName, StringComparison.CurrentCultureIgnoreCase))
                {
                    client.Value.Connection.Update(value, userName);
                }
            }
        }

        public List<string> GetCurrentUsers()
        {
            return ConnectedClients.Select(client => client.Value.UserName).ToList();
        }

        public bool Register(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            if (_db.Clients.Any(u => u.UserName == userName))
            {
                return false;
            }
            Client newUser = new Client
            {
                UserName = userName,
                Password = password
            };
            _db.Clients.Add(newUser);
            Save();
            return true;
        }

        public bool UserExists(string username)
        {
            return !string.IsNullOrEmpty(username) && _db.Clients.Any(u => u.UserName == username);
        }

        private void Save()
        {
            _db.SaveChanges();
        }
        // Non interface member area//


        public List<string> GetUserNames()
        {
            return new List<string>(_db.Clients.Select(u => u.UserName).ToList());
        }

        public bool BanUser(string username)
        {
            try
            {
                var userToBan = _db.Clients.FirstOrDefault(u => u.UserName == username);
                _db.Clients.Remove(userToBan);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Rename(string currentname, string newname)
        {
            try
            {
                var user = _db.Clients.FirstOrDefault(u => u.UserName == currentname);
                if (user != null) user.UserName = newname;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Client GetUserByName(string username)
        {
            try
            {
                return _db.Clients.FirstOrDefault(u => u.UserName == username);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void WipeUsers()
        {
            try
            {
                _db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Clients]");
                _db.SaveChanges();
                Console.WriteLine("Data entries deleted");
            }
            catch (Exception)
            {
                Console.WriteLine("Database error, no data was deleted.");
            }
        }

        public void LogoutUser(string username)
        {
            Client client = _db.Clients.FirstOrDefault(u => u.UserName == username);
            if (client == null) return;
            Client removedClient;
            ConnectedClients.TryRemove(client.UserName, out removedClient);

            UpdateHelper(false, removedClient.UserName);
            client.LoggedIn = false;
                
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Client log-off: {0} at {1}", removedClient.UserName, DateTime.Now);
            Console.ResetColor();
        }        
    }
}
