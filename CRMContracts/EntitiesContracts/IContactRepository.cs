using CRMEntities.Models;
using CRMModels;
using CRMModels.DataTransfersObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IContactRepository
    {

        void CreateContact(Contact contact);
        PagedList<Contact> GetContacts(ContactParameters contactParameters, bool trackChanges);
        Task<Contact> GetContactByIdAsync(long contactId, bool trackChanges);
        void MarkModified(Contact contact, UpdateContactDto contactDto);
        void CreateContacts(List<Contact> contacts);
        Task<bool> GetContactByNumberAndCreatedBy(string mobileNumber, string officeNumber, string homeNumber, long cretedBy);
        Task<List<Contact>> GetContactsByCompanyIdAsync(long companyId, bool trackChanges);
    }
}
