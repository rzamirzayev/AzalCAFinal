using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    class ServiceRepository : AsyncRepository<ServiceClass>, IServiceRepository
    {
        public ServiceRepository(DbContext db) : base(db)
        {
        }
    }
}
