using CRMEntities.Models;
using CRMModels;
using CRMModels.DataTransfersObjects;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IContactRepository
    {

        void CreateContact(Contact contact);
        PagedList<Contact> GetContacts(ContactParameters contactParameters, bool trackChanges);
        Task<Contact> GetContactByIdAsync(long contactId, bool trackChanges);
        void MarkModified(Contact contact, UpdateContactDto contactDto);
    }
}
