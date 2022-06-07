using CRMContracts.Email;
using EmailService.ExceptionHandling;
using EmailService.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SmartFormat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailServiceImplementation : IEmailService
    {
        private readonly IEmailConfiguration _mailSettings;

        public EmailServiceImplementation(IEmailConfiguration emailConfiguration)
        {
            _mailSettings = emailConfiguration;
        }
        public async Task SendEmailAsync(IEmailModel emailModel)
        {
            await Task.Delay(0);

            var htmlContent = GetEmailTemplate(emailModel);

            var recipientPayloadMappings = emailModel.Prepare();
            foreach (var item in recipientPayloadMappings)
            {
                var formattedContent = Smart.Format(htmlContent, item.Payload);

                await SendEmailToRecipient(item.Recipient, item.Subject, formattedContent,emailModel.Attachments);
            }

        }
        private string GetEmailTemplate(IEmailModel emailModel)
        {
            var loc = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var path = Path.Combine(loc, "EmailTemplates", $"{emailModel.TemplateName}.html");
            if (!File.Exists(path))
                throw new EmailTemplateMissingException(path);
            var htmlContent = File.ReadAllText(path);

            return htmlContent;
        }

        private async Task<bool> SendEmailToRecipient(IRecipient recipient, string subject, string formattedContent, List<string> attachments)
        {
            try
            {
                return await Send(new EmailModel
                {
                    Recipient = recipient,
                    Content = formattedContent,
                    Subject = subject,
                    Attachments = attachments
                });
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        private async Task<bool> Send(EmailModel model)
        {
            int emailPort = string.IsNullOrEmpty(_mailSettings.Port) ? 80 : int.Parse(_mailSettings.Port);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(model.Recipient.Email));
            email.Subject = model.Subject;
            var builder = new BodyBuilder();
            if (model.Attachments != null)
            {
                foreach (var fileName in model.Attachments)
                {
                     builder.Attachments.Add(fileName);
                }
            }
            builder.HtmlBody = model.Content;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, emailPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

            return true;// result.FirstOrDefault()?.Status != EmailResultStatus.Rejected;
        }
    }
}
 