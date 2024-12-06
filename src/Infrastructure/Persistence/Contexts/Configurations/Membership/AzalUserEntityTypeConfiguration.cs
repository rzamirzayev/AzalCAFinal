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
    class AzalUserEntityTypeConfiguration : IEntityTypeConfiguration<AzalUser>
    {
        public void Configure(EntityTypeBuilder<AzalUser> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable("Users", "Membership");
        }
    }
}
