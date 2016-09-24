using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DchatDb
{
    public class ChatRoom
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Guid ChatRoomId { get; set; }
        [Required]
        public string RoomName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
