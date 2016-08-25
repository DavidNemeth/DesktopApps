using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChattingInterfaces;

namespace ChattingServer.ServiceModel
{
    public class Client
    {
        public IClientService Connection;
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public System.Guid UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public byte[] Image { get; set; }   
             
        public bool LoggedIn { get; set; }

        public string Message { get; set; }
    }
}
