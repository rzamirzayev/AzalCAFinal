using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface IContactPostRepository
    {
        IQueryable<ContactPost> GetAll();
        Task<ContactPost> GetByIdAsync(int id,CancellationToken cancellationToken=default);
        void AddAsync(ContactPost entry, CancellationToken cancellationToken = default);
        void Edit(ContactPost entry);
        void Remove(ContactPost entry);
        Task<int> SaveAsync();


    }
}
