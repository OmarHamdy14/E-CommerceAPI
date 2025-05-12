using System.Linq.Expressions;

namespace ECommerceAPI.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>>? filter, string? IncludeProperties = null, bool IsTracked = false);
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null);
        Task Create(T entity);
        Task Update(T entity);
        Task Remove(T entity);
        Task RemoveRange(List<T> entities);
    }
}
