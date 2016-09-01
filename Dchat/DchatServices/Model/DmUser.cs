using DchatServices.Services;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DchatServices.Model
{
    [DataContract]
    public class DmUser
    {
        [DataMember]
        public IClientService Connection;
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public bool LoggedIn { get; set; }
        [DataMember]
        public byte[] Image { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public List<DmUser> IgnoreList { get; set; }
        [DataMember]
        public List<DmUser> FriendList { get; set; }
        [DataMember]
        public List<DmChatRoom> Rooms { get; set; }
    }
}
