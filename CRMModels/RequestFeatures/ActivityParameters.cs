namespace CRMModels
{
    public class ActivityParameters : RequestParameters
    {
        public ActivityParameters()
        {
            OrderBy = "id";
            SearchBy = "contactId";
        }
        public long ContactId { get; set; }
    }
}
