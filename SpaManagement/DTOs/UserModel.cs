namespace SpaManagement.DTOs
{
    public class UserModel: AccountModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
