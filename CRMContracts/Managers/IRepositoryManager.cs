using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IContactRepository Contact { get; }
        IAttachmentRepository Attachment { get; }
        IJobRepository Job { get; }
        IEventRepository Event { get; }
        IWorkOrderRepository WorkOrder { get; }
        IActivityRepository Activity { get; }
        Task<int> SaveAsync();
    }
}
