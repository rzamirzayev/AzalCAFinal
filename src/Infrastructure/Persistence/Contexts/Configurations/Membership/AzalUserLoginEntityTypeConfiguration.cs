using Domain.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts.Configurations.Membership
{
    class AzalUserLoginEntityTypeConfiguration : IEntityTypeConfiguration<AzalUserLogin>
    {
        public void Configure(EntityTypeBuilder<AzalUserLogin> builder)
        {
            builder.ToTable("UserLogins", "Membership");
        }
    }
}
