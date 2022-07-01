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
        private IJobRepository _jobRepository;
        private IEventRepository _eventRepository;
        private IWorkOrderRepository _workOrderRepository;
        private IActivityRepository _activityRepository;
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
        public IJobRepository Job
        {
            get
            {
                if (_jobRepository == null)
                    _jobRepository = new JobRepository(_repositoryContext);
                return _jobRepository;
            }
        }
        public IEventRepository Event
        {
            get
            {
                if (_eventRepository == null)
                    _eventRepository = new EventRepository(_repositoryContext);
                return _eventRepository;
            }
        }
        public IWorkOrderRepository WorkOrder
        {
            get
            {
                if (_workOrderRepository == null)
                    _workOrderRepository = new WorkOrderRepository(_repositoryContext);
                return _workOrderRepository;
            }
        }
        public IActivityRepository Activity
        {
            get
            {
                if (_activityRepository == null)
                    _activityRepository = new ActivityRepository(_repositoryContext);
                return _activityRepository;
            }
        }
        public Task<int> SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}
