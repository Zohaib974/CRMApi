using CRMContracts;
using CRMEntities;
using CRMEntities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMRepository
{
    public class UserColumnRepository : RepositoryBase<UserColumn>, IUserColumnRepository
    {
        public UserColumnRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void CreateUserColumns(List<UserColumn> entities) => CreateEntities(entities);

        public Task<List<UserColumn>> GetUserColumnsByUserIdAsync(long Userid, int tableType, bool trackChanges)
        {
            var dataList = FindByCondition(e => !e.IsDeleted && (e.UserId == Userid
                                    && e.TableType == tableType), trackChanges)
                                    .OrderBy(e=>e.ColumnOrder)
                                    .ToListAsync();
            return dataList;
        }
    }
}
