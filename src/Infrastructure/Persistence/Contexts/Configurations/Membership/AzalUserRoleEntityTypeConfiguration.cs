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
    class AzalUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<AzalUserRole>
    {
        public void Configure(EntityTypeBuilder<AzalUserRole> builder)
        {
            builder.HasKey(m => new { m.UserId,m.RoleId }) ;
            builder.ToTable("UserRoles", "Membership");
        }
    }
}
