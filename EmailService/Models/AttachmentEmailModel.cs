using CRMContracts.Email;
using System.Collections.Generic;

namespace EmailService.Models
{
    public class AttachmentEmailModel : IEmailModel
    {
        private readonly IRecipient _recipient;

        public AttachmentEmailModel(IRecipient recipient)
        {
            this._recipient = recipient;
            Attachments = new List<string>();
        }

        public string TemplateName { get; set; } = "Attachment";
        public List<string> Attachments { get; set; }

        public IList<IEmailRecipientPayloadInfo> Prepare()
        {
            IList<IEmailRecipientPayloadInfo> list = new List<IEmailRecipientPayloadInfo>();

            var payload = new Dictionary<string, object>();
            payload.Add("DocumentName", _recipient.FullName);

            list.Add(new EmailRecipientPayloadInfo(_recipient, Subject(), payload));

            return list;
        }

        private string Subject()
        {
            return "Accura Core Document";
        }
    }
}
