using CRMModels.DataTransfersObjects;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IServiceManager
    {
        public Task<ContactDto> CreateContactAsync(CreateContactDto contact);
    }
}
