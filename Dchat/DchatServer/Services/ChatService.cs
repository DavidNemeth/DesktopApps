using AutoMapper;
using DchatClient.DchatInterface;
using DchatEntities;
using DchatServer.DchatInterface;
using DchatServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace DchatServer.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        public List<DmUser> ConnectedUsers = new List<DmUser>();

        private readonly DchatContext _db = new DchatContext();


        /**
         * 0: Log in Completed
         * 1: Invalid username or password
         * 2: User already logged in
         * 3: Server offline
         **/
        public int Login(string username, string password)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
                var dmuser = Mapper.Map<DmUser>(user);
                if (user == null)
                {
                    return 1;
                }
                if (ConnectedUsers.Contains(dmuser))
                {
                    return 2;
                }
                var userlogin = Mapper.Map<DmUser>(user);

                var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClientService>();

                userlogin.Connection = establishedUserConnection;
                ConnectedUsers.Add(dmuser);
                userlogin.LoggedIn = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Client login: {0} with id: {1} at {2}", user.Username, user.UserId, DateTime.Now);
                Console.ResetColor();
                return 0;
            }
            catch (Exception)
            {
                return 3;
            }
        }

        public void Logout()
        {
            try
            {
                var dmuser = GetMyClient();
                if (dmuser == null) return;
                ConnectedUsers.Remove(dmuser);
                dmuser.LoggedIn = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Client log-off: {dmuser.Username} at {DateTime.Now}");
                Console.ResetColor();
            }
            catch (Exception)
            {
                //ignore                
            }
        }        

        /**
        * 0: Log in Completed
        * 1: User already exists
        * 2: Server offline      
        **/
        public int Register(string username, string password)
        {
            if (_db.Users.FirstOrDefault(u => u.Username == username) != null)
            {
                return 1;
            }
            try
            {
                var user = new User();
                user.Username = username;
                user.Password = password;
                return 0;
            }
            catch (Exception)
            {
                return 2;
            }
        }

        public void SendMessageToAll(string message, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return;
            }
            foreach (var client in ConnectedUsers)
            {
                if (!String.Equals(client.Username, userName, StringComparison.CurrentCultureIgnoreCase))
                {
                    Mapper.Map<DmUser>(client).Connection.GetMessage(message, userName);
                }
            }
        }

        public DmUser GetUserByName(string username)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(u => u.Username == username);
                return Mapper.Map<DmUser>(user);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<DmUser> GetCurrentUsers()
        {            
            return ConnectedUsers;
        }

        public List<DmUser> GetFriendList()
        {
            return ConnectedUsers.SelectMany(u => u.FriendList).ToList();
        }

        public List<DmUser> GetIgnoreList()
        {
            return ConnectedUsers.SelectMany(u => u.IgnoreList).ToList();
        }

        #region Server area

        public bool UserExists(string username)
        {
            return !string.IsNullOrEmpty(username) && _db.Users.Any(u => u.Username == username);
        }

        public bool BanUser(string username)
        {
            try
            {
                var userToBan = _db.Users.FirstOrDefault(u => u.Username == username);
                _db.Users.Remove(userToBan);
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
                var user = _db.Users.FirstOrDefault(u => u.Username == currentname);
                if (user != null) user.Username = newname;
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void WipeUsers()
        {
            try
            {
                _db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Users]");
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
            User client = _db.Users.FirstOrDefault(u => u.Username == username);
            DmUser dmuser = Mapper.Map<DmUser>(client);
            if (client == null) return;            
            ConnectedUsers.Remove(dmuser);

            UpdateHelper(false, dmuser.Username);
            client.LoggedIn = false;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Client log-off: {0} at {1}", dmuser.Username, DateTime.Now);
            Console.ResetColor();
        }

        #endregion
        #region private

        private DmUser GetMyClient()
        {
            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClientService>();
            return (from client in ConnectedUsers where client.Connection == establishedUserConnection select client).FirstOrDefault();
        }

        private void UpdateHelper(bool value, string userName)
        {
            foreach (var client in ConnectedUsers)
            {
                if (!string.Equals(client.Username, userName, StringComparison.CurrentCultureIgnoreCase))
                {
                    client.Connection.Update(value, userName);
                }
            }
        }        

        private void Save()
        {
            _db.SaveChanges();
        }

        #endregion
    }
}
