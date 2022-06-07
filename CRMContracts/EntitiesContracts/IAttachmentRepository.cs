using CRMEntities.Models;
using CRMModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IAttachmentRepository
    {
        void AddAttchmentsAsync(List<Attachment> attachment);
        PagedList<Attachment> GetAttachments(AttachmentParameters attachmentParameters, bool trackChanges);
        Task<List<Attachment>> GetByIdsAsync(IEnumerable<long> ids, bool trackChanges);
        Task<Attachment> GetByIdAsync(long id, bool trackChanges);
    }
}
