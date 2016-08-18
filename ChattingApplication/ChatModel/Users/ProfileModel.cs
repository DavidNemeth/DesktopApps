using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatModel.Users
{
    public class ProfileModel
    {
        [Key]
        public Guid UserID { get; set; }
        [Required]
        public string Nick { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public byte[] Image { get; set; }
        public bool LoggedIn { get; set; }
        public string Message { get; set; }
    }
}
