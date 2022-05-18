using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IContactRepository Contact { get; }
        Task<int> SaveAsync();
    }
}
