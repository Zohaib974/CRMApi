using System;

namespace CRMModels.DataTransfersObjects
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public object FullAddress { get; set; }
    }
}
