using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using SpaManagement.Domain.Configuration;
using SpaManagement.Domain.Model;

namespace SpaManagement.Domain.EmailHelper
{
    public class EmailHelper : IEmailHelper
    {
        EmailConfig _emailConfig;

        public EmailHelper(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendEmail(EmailRequest emailRequest)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(_emailConfig.Provider, _emailConfig.Port);
                smtpClient.Credentials = new NetworkCredential(_emailConfig.DefaultSender, _emailConfig.Password);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(_emailConfig.DefaultSender);
                mailMessage.To.Add(emailRequest.To);
                mailMessage.Subject = emailRequest.Subject;
                mailMessage.Body = emailRequest.Content;

                if (emailRequest.AttachmentFilePaths.Length > 0)
                {
                    foreach (var path in emailRequest.AttachmentFilePaths)
                    {
                        Attachment attachment = new Attachment(path);
                        mailMessage.Attachments.Add(attachment);
                    }
                }

                await smtpClient.SendMailAsync(mailMessage);

                mailMessage.Dispose();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
