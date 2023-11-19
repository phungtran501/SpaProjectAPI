namespace SpaManagement.Service.DTOs
{
    public class AccountDTO
    {
        public string? Id { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        public string RoleName { get; set; }
    }
}
