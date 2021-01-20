using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsApi.Repos
{
    //ADd link to Solid
    //Async Await Pattern
    interface IRepository<T> where T: class
    {
        Task<List<T>> GetAllAsync(string partition);

        Task<T> GetByIdAsync(string id, string partition);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

    }

    interface IRepositoryOfT<T> where T : class
    {
        Task<List<T>> GetAllAsync(string partition);

        Task<T> GetByIdAsync(string id, string partition);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

    }
}
