﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Common
{
    public abstract class AsyncRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly DbContext db;

        public AsyncRepository(DbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(T entry, CancellationToken cancellationToken = default)
        {
            await db.Set<T>().AddAsync(entry, cancellationToken);
        }

        public void Edit(T entry)
        {
            db.Entry(entry).State = EntityState.Modified;
        }

        public IQueryable<T> GetAll()
        {
            var query = db.Set<T>().AsQueryable();
            return query;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            var query = db.Set<T>().AsQueryable();
            if (predicate is not null)
            {
                query = query.Where(predicate);
            }
            var entry = await query.FirstOrDefaultAsync(cancellationToken);
            if(entry is null) {
                throw new ArgumentNullException();
            }
            return entry;
        }

//        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
//        {
//            var query = db.Set<T>().AsQueryable();
//            var entry = await query.FirstOrDefaultAsync(m => m.id == id, cancellationToken);
//#warning entry null ola biler
//            return entry;
//        }

        public void Remove(T entry)
        {
            db.Set<T>().Remove(entry);
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await db.SaveChangesAsync(cancellationToken);
        }
    }
}
