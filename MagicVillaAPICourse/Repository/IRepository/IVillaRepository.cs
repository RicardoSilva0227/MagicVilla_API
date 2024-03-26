using MagicVillaAPICourse.Models;
using System.Linq.Expressions;

namespace MagicVillaAPICourse.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);

    }
}
