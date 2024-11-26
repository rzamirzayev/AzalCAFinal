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
    class AirportRepository : AsyncRepository<Airport>, IAirportRepoitory
    {
        public AirportRepository(DbContext db) : base(db)
        {
        }
  
    }
}
