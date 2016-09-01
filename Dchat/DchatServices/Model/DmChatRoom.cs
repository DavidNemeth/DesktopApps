using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DchatServices.Model
{
    [DataContract]
    public class DmChatRoom
    {
        [DataMember]
        public Guid ChatRoomId { get; set; }
        [DataMember]
        public string RoomName { get; set; }
        [DataMember]
        public List<DmUser> Users { get; set; }
    }
}
