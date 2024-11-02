using System.ComponentModel.DataAnnotations;

namespace MygRPC.Models
{
    public class Customer
    {
        [Key]
        public string CusId { get; set; }
        public string CusName { get; set; }
        public string CusGender { get; set; }
        public DateTime CusBirthday { get; set; }
        public string CusAddress { get; set; }
    }
}