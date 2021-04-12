using FA.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.Core.Infrastructure
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        DbSet<T> Table();
        Task SaveChangesAsync();
        Task<T> GetByIdAsync(Guid id);
        IQueryable<T> All();
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
