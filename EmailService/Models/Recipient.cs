using CRMContracts.Email;

namespace EmailService.Models
{
    public class Recipient : IRecipient
    {
        public Recipient(string fullName, string email)
        {
            this.FullName = fullName;
            this.Email = email;
        }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
