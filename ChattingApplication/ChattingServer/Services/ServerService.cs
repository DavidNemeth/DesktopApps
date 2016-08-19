using ChatModel;
using ChatModel.Users;
using ChattingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ChattingServer.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServerService" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ServerService : IServerService
    {
        ProfilesContext db = new ProfilesContext();
        public List<ProfileModel> connectedClients = new List<ProfileModel>();

        public bool Login(string userName, string password)
        {
            ProfileModel user = db.Profiles.FirstOrDefault(p => p.Nick == userName && p.Password == password);
            if (user == null)
            {
                return false;
            }
            else
            {
                var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClientService>();

                user.connection = establishedUserConnection;
                connectedClients.Add(user);
                //updateHelper(0, userName);

                user.LoggedIn = true;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Client login: {0} with id: {1} at {2}", user.Nick, user.UserID, DateTime.Now);
                Console.ResetColor();
                return true;
            }
        }

        public List<ProfileModel> GetCurrentUsers()
        {
            return db.Profiles
                .Where(p => p.LoggedIn == true).ToList();
        }

        public void Logout()
        {
            ProfileModel user = GetUser();
            if (user != null)
            {
                connectedClients.Remove(user);

                //updateHelper(1, removedClient.UserName);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Client logoff: {0} with id: {1} at {2}", user.Nick, user.UserID, DateTime.Now);
                Console.ResetColor();
            }
        }

        public void Register(string userName, string password)
        {
            ProfileModel newUser = new ProfileModel();
            newUser.Nick = userName;
            newUser.Password = password;
            Save();            
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void SendMessageToAll(string message, string userName)
        {
            foreach (var client in connectedClients)
            {
                if (client.Nick.ToLower() != userName.ToLower())
                {
                    client.connection.GetMessage(message, userName);
                }
            }
        }

        public ProfileModel GetUser()
        {
            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClientService>();

            foreach (var client in connectedClients)
            {
                if (client.connection == establishedUserConnection)
                {
                    return client;
                }
            }
            return null;
        }

        private void updateHelper(bool value, string userName)
        {
            foreach (var client in connectedClients)
            {
                if (client.Nick.ToLower() != userName.ToLower())
                {
                    client.connection.Update(value, userName);
                }
            }
        }
    }
}
