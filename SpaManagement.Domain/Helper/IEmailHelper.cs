using SpaManagement.Domain.Model;

namespace SpaManagement.Domain.EmailHelper
{
    public interface IEmailHelper
    {
        Task SendEmail(EmailRequest emailRequest);
    }
}