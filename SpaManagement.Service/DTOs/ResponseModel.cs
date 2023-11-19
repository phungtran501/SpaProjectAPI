using SpaManagement.Domain.Enums;

namespace SpaManagement.Service.DTOs
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public StatusType StatusType { get; set; }

        public ActionType Action { get; set; }
    }
}
