namespace CRMModels
{
    public class WorkOrderParameters : RequestParameters
    {
        public WorkOrderParameters()
        {
            OrderBy = "id";
            SearchBy = "name";
        }
        public long ContactId { get; set; }
    }
}
