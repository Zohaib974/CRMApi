﻿using CRMServices.DataTransferObjects;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
    }

}
