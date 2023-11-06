using System.ComponentModel.DataAnnotations;

namespace SpaManagement.DTOs
{
    public class AccountModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
