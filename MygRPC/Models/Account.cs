using System.ComponentModel.DataAnnotations;

namespace MygRPC.Models
{
    public class Account
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
