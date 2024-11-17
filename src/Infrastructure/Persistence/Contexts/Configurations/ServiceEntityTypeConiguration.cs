using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts.Configurations
{
    public class ServiceEntityTypeConiguration:IEntityTypeConfiguration<ServiceClass>
    {
        public void Configure(EntityTypeBuilder<ServiceClass> builder)
        {
            builder.Property(s => s.Id).HasColumnType("int").UseIdentityColumn(1, 1);
            builder.Property(s => s.Name).HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            builder.Property(s => s.Title).HasColumnType("nvarchar").IsRequired().HasMaxLength(200);
            builder.Property(s => s.Description).HasColumnType("nvarchar").HasMaxLength(500);
            builder.Property(s => s.CreatedAt).HasColumnType("datetime").IsRequired();


            builder.ToTable("Services");
            builder.HasKey(s => s.Id);
        }
    }
}
