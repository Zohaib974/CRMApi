using CRMContracts;
using CRMEntities;
using System.Threading.Tasks;

namespace CRMRepository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private IContactRepository _contactRepository;
        private IAttachmentRepository _attachmentRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }
        public IContactRepository Contact
        {
            get
            {
                if (_contactRepository == null)
                    _contactRepository = new ContactRepository(_repositoryContext);
                return _contactRepository;
            }
        }
        public IAttachmentRepository Attachment
        {
            get
            {
                if (_attachmentRepository == null)
                    _attachmentRepository = new AttachmentRepository(_repositoryContext);
                return _attachmentRepository;
            }
        }
        public Task<int> SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
