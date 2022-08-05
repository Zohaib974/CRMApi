using System.Collections.Generic;

namespace CRMModels.Common
{
    public class ListResponse<T> where T : class
    {
        public ListResponse(List<T> list)
        {
            items = list;
        }
        public List<T> items { get; set; }
        public bool Successful { get; set; } = true;
        public string Message { get; set; }
    }

}

