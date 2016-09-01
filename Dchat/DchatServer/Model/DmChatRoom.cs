using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DchatServer.Model
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
