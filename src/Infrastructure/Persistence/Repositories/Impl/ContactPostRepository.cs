using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Impl
{
    class ContactPostRepository : IContactPostRepository
    {
        public void AddAsync(ContactPost entry, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Edit(ContactPost entry)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ContactPost> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ContactPost> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(ContactPost entry)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
