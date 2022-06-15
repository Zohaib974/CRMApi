using CRMContracts.Email;
using System.Collections.Generic;

namespace EmailService.Models
{
    public class EmailModel
    {
        public IRecipient Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<string> Attachments { get; set; }
    }
}
