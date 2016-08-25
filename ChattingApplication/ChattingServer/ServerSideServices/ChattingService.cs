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
            try
            {
                Client user = db.Clients.FirstOrDefault(p => p.UserName == userName && p.Password == password);
                if (user == null || _connectedClients.Values.Where(u => u.UserName == userName).FirstOrDefault() != null)
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
            catch (Exception)
            {
                return false;
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
            if (string.IsNullOrEmpty(userName))
            {
                return;
            }
            foreach (var client in _connectedClients)
            {
                if (client.Key.ToLower() != userName.ToLower())
                {
                    client.Value.connection.GetMessage(message, userName);
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

        public bool Register(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            if (db.Clients.Any(u => u.UserName == userName))
            {
                return false;
            }
            else
            {
                Client newUser = new Client();
                newUser.UserName = userName;
                newUser.Password = password;
                db.Clients.Add(newUser);
                Save();
                return true;
            }
        }

        public bool UserExists(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            if (db.Clients.Any(u => u.UserName == username))
                return true;
            else
                return false;
        }

        private void Save()
        {
            db.SaveChanges();
        }
        // Non interface member area//


        public List<string> GetUserNames()
        {
            return new List<string>(db.Clients.Select(u => u.UserName).ToList());
        }

        public bool BanUser(string username)
        {
            try
            {
                var userToBan = db.Clients.Where(u => u.UserName == username).FirstOrDefault();
                db.Clients.Remove(userToBan);
                db.SaveChanges();
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
                Client user = db.Clients.Where(u => u.UserName == currentname).FirstOrDefault();
                user.UserName = newname;
                db.SaveChanges();
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
                return db.Clients.Where(U => U.UserName == username).FirstOrDefault();
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
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Clients]");
                db.SaveChanges();
                Console.WriteLine("Data entries deleted");
            }
            catch (Exception)
            {
                Console.WriteLine("Database error, no data was deleted.");
            }
        }

        public void LogoutUser(string username)
        {
            Client client = db.Clients.Where(u => u.UserName == username).FirstOrDefault();
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
    }
}
