using CRMContracts;
using CRMEntities;
using CRMEntities.Models;

namespace CRMRepository
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {

        public ContactRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void CreateContact(Contact contact) => Create(contact);
    }
}
