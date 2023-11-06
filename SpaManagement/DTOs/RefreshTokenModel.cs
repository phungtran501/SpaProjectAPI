using System.ComponentModel.DataAnnotations;

namespace SpaManagement.DTOs
{
    public class RefreshTokenModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
