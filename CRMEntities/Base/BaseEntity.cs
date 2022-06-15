using System;

namespace CRMEntities.Base
{
    public abstract class BaseEntity : IEntity
    {
        public virtual long Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        protected BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public void SetDeletedTracking(long deletedBy)
        {
            DeletedBy = deletedBy;
            DeletedOn = DateTime.UtcNow;
        }
        public void SetModificationTracking(long modifiedBy)
        {
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTime.UtcNow;
        }
    }
}
