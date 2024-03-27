using MagicVillaAPICourse.Models;
using System.Linq.Expressions;

namespace MagicVillaAPICourse.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}
