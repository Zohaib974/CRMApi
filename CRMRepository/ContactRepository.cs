using CRMContracts;
using CRMEntities;
using CRMEntities.Models;
using CRMModels;
using CRMModels.DataTransfersObjects;
using CRMRepository.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public void CreateContacts(List<Contact> contacts)
        {
            CreateEntities(contacts);
        }
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
        public async Task<List<Contact>> GetContactsByCompanyIdAsync(long companyId, bool trackChanges)
        {
            return await FindByCondition(e => !e.IsDeleted && e.CompanyId.Equals(companyId), trackChanges).ToListAsync();
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

        public async Task<bool> GetContactByNumberAndCreatedBy(string mobileNumber, string officeNumber, string homeNumber, long createdBy)
        {
            List<string> numbers = new List<string>{ mobileNumber ?? String.Empty,officeNumber ?? String.Empty, homeNumber ?? String.Empty };
            return await FindByCondition(e => !e.IsDeleted && 
                                        (numbers.Contains(e.MobileNumber) || numbers.Contains(e.OfficeNumber) || numbers.Contains(e.HomeNumber)) &&
                                        e.CreatedBy == createdBy, false)
                                        .AnyAsync();
        }

        public List<Contact> GetContactsIdsAsync(List<long> Ids, bool trackChanges)
        {
            return FindByCondition(e => !e.IsDeleted && Ids != null && Ids.Contains(e.Id), trackChanges).ToList();
        }
        #endregion
    }
}
