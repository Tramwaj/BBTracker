using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BBTracker.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBTracker.Configurations
{
    class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).HasMaxLength(20);
            builder.Property(x => x.LastName).HasMaxLength(30);
            builder.Property(x => x.Nick).HasMaxLength(30);
        }
    }
}
