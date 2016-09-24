using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DchatDb
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool LoggedIn { get; set; }

        public byte[] Image { get; set; }

        public string Message { get; set; }

        public ICollection<User> IgnoreList { get; set; }

        public ICollection<User> FriendList { get; set; }

        public ICollection<ChatRoom> Rooms { get; set; }
    }
}
