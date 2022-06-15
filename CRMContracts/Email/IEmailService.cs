using System.Threading.Tasks;

namespace CRMContracts.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(IEmailModel model);
    }
}
