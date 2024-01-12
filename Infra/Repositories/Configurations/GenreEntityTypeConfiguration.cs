using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repositories.Configurations
{
    public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder.HasMany(g => g.Books)
                   .WithOne(b => b.Genre)
                   .HasForeignKey(b => b.GenreId)
                   .IsRequired();

            builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(120)
                    .HasColumnName("Name");
        }
    }
}