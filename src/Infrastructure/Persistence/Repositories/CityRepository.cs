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
    class CityRepository : AsyncRepository<City>, ICityRepository
    {
        public CityRepository(DbContext db) : base(db)
        {
        }
    }
}
