using CRMContracts;
using CRMEntities;
using CRMEntities.Models;
using CRMModels;
using CRMRepository.Extensions;
using System.Linq;

namespace CRMRepository
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {

        public ContactRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void CreateContact(Contact contact) => Create(contact);

        public PagedList<Contact> GetContacts(ContactParameters contactParameters, bool trackChanges = false)
        {
            var contacts = FindAll(trackChanges)
                                    .Search(contactParameters.SearchBy,contactParameters.SearchTerm)
                                    .Sort(contactParameters.OrderBy)
                                    .ToList();

            return PagedList<Contact>.ToPagedList(contacts, contactParameters.PageNumber, contactParameters.PageSize);
        }
    }
}
