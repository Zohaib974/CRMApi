namespace CRMModels
{
    public class JobParameters : RequestParameters
    {
        public JobParameters()
        {
            OrderBy = "id";
            SearchBy = "name";
        }
        public long PrimaryContactId { get; set; }
    }
}
