using CRMEntities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMContracts
{
    public interface IUserColumnRepository
    {
        void CreateUserColumns(List<UserColumn> entity);
        Task<List<UserColumn>> GetUserColumnsByUserIdAsync(long Userid, int tableType, bool trackChanges);
    }
}
