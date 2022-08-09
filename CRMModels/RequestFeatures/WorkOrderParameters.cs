using CRMModels.Common;

namespace CRMModels
{
    public class WorkOrderParameters : RequestParameters
    {
        public WorkOrderParameters()
        {
            OrderBy = "id";
            SearchBy = "name";
        }
        public long ReferenceId { get; set; }
        public TableType ReferenceType { get; set; }
    }
}
