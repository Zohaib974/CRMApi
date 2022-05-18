using CRMContracts;
using CRMEntities;
using CRMEntities.Models;
using CRMModels;
using CRMModels.DataTransfersObjects;
using CRMRepository.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
            var contacts = FindByCondition(e => !e.IsDeleted,trackChanges)
                                    .Search(contactParameters.SearchBy,contactParameters.SearchTerm)
                                    .Sort(contactParameters.OrderBy)
                                    .ToList();

            return PagedList<Contact>.ToPagedList(contacts, contactParameters.PageNumber, contactParameters.PageSize);
        }

        public async Task<Contact> GetContactByIdAsync(long contactId, bool trackChanges)
        {
            return await FindByCondition(e => !e.IsDeleted && e.Id.Equals(contactId), trackChanges).SingleOrDefaultAsync();
        }
        #region helperMethod
        public void MarkModified(Contact contact,UpdateContactDto contactDto)
        {
            RepositoryContext.Entry(contact).State = EntityState.Modified;
            var entityProperties = typeof(Contact).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dtoProperties = typeof(UpdateContactDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(var dProp in dtoProperties)
            {
                 var dPropValue = dProp.GetValue(contactDto);
                if(dPropValue == null)
                {
                    var entityProperty = entityProperties.FirstOrDefault(pi => pi.Name.Equals(dProp.Name, StringComparison.InvariantCultureIgnoreCase));
                    if(entityProperty == null)
                        continue;
                    var epropName = entityProperty.Name.ToString();
                    RepositoryContext.Entry(contact).Property(epropName).IsModified = false;
                }
            }
        }
        #endregion
    }
}
