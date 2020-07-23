using BBTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.Configurations
{
    class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Start).IsRequired();            
        }
    }
}
