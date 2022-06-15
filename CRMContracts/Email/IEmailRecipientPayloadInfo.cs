using System.Collections.Generic;

namespace CRMContracts.Email
{
    public interface IEmailRecipientPayloadInfo
    {
        IRecipient Recipient { get; }
        string Subject { get; }
        Dictionary<string, object> Payload { get; }
    }
}
