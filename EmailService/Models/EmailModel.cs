using CRMContracts.Email;

namespace EmailService.Models
{
    public class EmailModel
    {
        public IRecipient Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public dynamic Attachments { get; set; }
    }
}
