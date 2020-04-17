using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Cynosura.Core.Services;
using Cynosura.Template.Core.Email;
using Cynosura.Template.Core.Email.Models;
using Microsoft.Extensions.Options;

namespace Cynosura.Template.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderOptions _options;

        public EmailSender(IOptions<EmailSenderOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendAsync(EmailModel model)
        {
            var client = new SmtpClient();
            if (string.IsNullOrEmpty(_options.PickupFolder))
            {
                client.Host = _options.SmtpServer;
                client.Port = _options.SmtpPort;
                client.Credentials = new NetworkCredential(_options.UserName, _options.Password);
                client.EnableSsl = _options.EnableSsl;
            }
            else
            {
                client.PickupDirectoryLocation = _options.PickupFolder;
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            }
            var fromAddress = new MailAddress(model.From);
            var toAddress = new MailAddress(model.To);
            var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Body = model.Body,
                Subject = model.Subject,
                IsBodyHtml = model.IsBodyHtml,
            };
            if (!string.IsNullOrEmpty(model.Cc))
            {
                mailMessage.CC.Add(new MailAddress(model.Cc));
            }
            if (!string.IsNullOrEmpty(model.Bcc))
            {
                mailMessage.Bcc.Add(new MailAddress(model.Bcc));
            }
            if (model.Attachments != null)
            {
                foreach (var attachment in model.Attachments)
                {
                    var stream = new MemoryStream(attachment.Data);
                    mailMessage.Attachments.Add(new Attachment(stream, attachment.FileName,
                        attachment.ContentType));
                }
            }

            await client.SendMailAsync(mailMessage);
        }
    }
}
