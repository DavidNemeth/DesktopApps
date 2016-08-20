using ChattingInterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChattingServer
{
    public class Client
    {
        public IClientService connection;
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public System.Guid UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public byte[] Image { get; set; }   
             
        public bool LoggedIn { get; set; }

        public string Message { get; set; }
    }
}
