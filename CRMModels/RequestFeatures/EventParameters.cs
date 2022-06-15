namespace CRMModels
{
    public class EventParameters : RequestParameters
    {
        public EventParameters()
        {
            OrderBy = "id";
            SearchBy = "eventName";
        }
        public long ContactId { get; set; }
    }
}
