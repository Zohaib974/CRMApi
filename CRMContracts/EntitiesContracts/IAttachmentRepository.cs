using CRMEntities.Models;
using CRMModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IAttachmentRepository
    {
        void AddAttchments(List<Attachment> attachment);
        PagedList<Attachment> GetAttachments(AttachmentParameters attachmentParameters, bool trackChanges);
        IEnumerable<Attachment> GetByIds(IEnumerable<long> ids, bool trackChanges);
    }
}
