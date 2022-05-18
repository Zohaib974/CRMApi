using CRMModels;
using CRMModels.Common;
using CRMModels.DataTransfersObjects;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IServiceManager
    {
        public Task<ContactDto> CreateContactAsync(CreateContactDto contact);
        CommmonListResponse<ContactDto> GetContacts(ContactParameters contactParameterss);
        Task<ContactDto> UpdateContactAsync(UpdateContactDto contact);
        Task<CommonResponse> DeleteContact(long id);
    }
}
