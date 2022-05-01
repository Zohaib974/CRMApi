using CRMContracts.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Models
{
    public class ResetPasswordEmailModel : IEmailModel
    {
        private readonly string _resetPasswordLink;
        private readonly IRecipient _recipient;

        public ResetPasswordEmailModel(string activationUrl, IRecipient recipient)
        {
            this._resetPasswordLink = activationUrl;
            this._recipient = recipient;
        }

        public string TemplateName { get; set; } = "ResetPassword";

        public IList<IEmailRecipientPayloadInfo> Prepare()
        {
            IList<IEmailRecipientPayloadInfo> list = new List<IEmailRecipientPayloadInfo>();

            var payload = new Dictionary<string, object>();
            payload.Add("Name", _recipient.FullName);
            payload.Add("PasswordLink", _resetPasswordLink);

            list.Add(new EmailRecipientPayloadInfo(_recipient, Subject(), payload));

            return list;
        }

        private string Subject()
        {
            return "Reset your account password";
        }
    }
}
