using CRMEntities.Models;
using CRMModels;

namespace CRMContracts
{
    public interface IContactRepository
    {

        void CreateContact(Contact contact);
        PagedList<Contact> GetContacts(ContactParameters contactParameters, bool trackChanges);
    }
}
