using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DchatEntities
{
    public class ChatRoom
    {        
        [Key]
        [Required]
        public Guid ChatRoomId { get; set; }
        [Required]
        public string RoomName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
