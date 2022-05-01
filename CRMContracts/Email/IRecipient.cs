using System;
using System.Collections.Generic;
using System.Text;

namespace CRMContracts.Email
{
    public interface IRecipient
    {
        string Email { get; set; }
        string FullName { get; set; }
    }
}
