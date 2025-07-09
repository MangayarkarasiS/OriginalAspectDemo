using System.ComponentModel.DataAnnotations;

namespace StudentService.Models
{
    public class UserCredentials
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
