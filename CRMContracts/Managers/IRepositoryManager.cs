using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IContactRepository Contact { get; }
        IAttachmentRepository Attachment { get; }
        Task<int> SaveAsync();
    }
}
