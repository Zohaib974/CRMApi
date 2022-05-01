using System.Collections.Generic;

namespace CRMContracts.Email
{
    public interface IEmailModel
    {
        IList<IEmailRecipientPayloadInfo> Prepare();

        string TemplateName { get; set; }
    }
}
