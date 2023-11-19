namespace SpaManagement.Service.DTOs
{
    public class AppointmentDTO
    {
        public int? Id { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
    }
}
