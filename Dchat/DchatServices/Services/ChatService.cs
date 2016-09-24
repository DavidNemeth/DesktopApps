using DchatServices.Model;
using DchatDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace DchatServices.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        public HashSet<DmUser> ConnectedUsers = new HashSet<DmUser>();
        private readonly DchatContext _db = new DchatContext();

        /**
         * 0: Log in Completed
         * 1: Invalid username or password
         * 2: User already logged in
         * 3: Server offline
         **/

        public string Login(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                return "Incorrect username or password";
            }
            foreach (var item in ConnectedUsers)
            {
                if (item.Username == username)
                {
                    return "User already logged in";
                }
            }
            try
            {
                var dmUser = new DmUser();
                dmUser.Username = user.Username;
                dmUser.Password = user.Password;
                dmUser.Image = user.Image;
                dmUser.LoggedIn = user.LoggedIn;
                dmUser.UserId = user.UserId;
                ConnectedUsers.Add(dmUser);
                dmUser.LoggedIn = true;
                //Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine("Client login: {0} with id: {1} at {2}", user.Username, user.UserId, DateTime.Now);
                //Console.ResetColor();
                return "Success";
            }
            catch (Exception e)
            {
                return e.ToString();
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
                //Console.ForegroundColor = ConsoleColor.Cyan;
                //Console.WriteLine($"Client log-off: {dmuser.Username} at {DateTime.Now}");
                //Console.ResetColor();
            }
            catch (Exception)
            {
                //ignore                
            }
        }

        public string Register(string username, string password)
        {
            if (_db.Users.FirstOrDefault(u => u.Username == username) != null)
            {
                return "User already exists";
            }
            try
            {
                var user = new User();
                user.Username = username;
                user.Password = password;
                user.LoggedIn = false;                
                _db.Users.Add(user);
                _db.SaveChanges();  
                return "Registration Complete!";
            }
            catch (Exception)
            {
                return "Server is offline";
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
                    client.Connection.GetMessage(message, userName);
                }
            }
        }

        public HashSet<DmUser> GetConnectedUsers()
        {
            return ConnectedUsers;
        }

        public DmUser GetUserByName(string username)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(u => u.Username == username);
                var dmUser = new DmUser();

                dmUser.Username = user.Username;
                dmUser.Password = user.Password;
                dmUser.Image = user.Image;
                dmUser.LoggedIn = user.LoggedIn;
                dmUser.UserId = user.UserId;

                return dmUser;
            }
            catch (Exception)
            {
                return null;
            }
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
            DmUser dmUser = ConnectedUsers.FirstOrDefault(u => u.Username == username);
            if (dmUser == null) return;
            ConnectedUsers.Remove(dmUser);

            UpdateHelper(false, dmUser.Username);
            dmUser.LoggedIn = false;

            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine("Client log-off: {0} at {1}", dmUser.Username, DateTime.Now);
            //Console.ResetColor();
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

        public void StartUp()
        {
        }

        #endregion
    }
}
