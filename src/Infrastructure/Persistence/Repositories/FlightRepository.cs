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
    class FlightRepository : AsyncRepository<Flight>, IFlightRepository
    {
        public FlightRepository(DbContext db) : base(db)
        {
        }
    }
}
