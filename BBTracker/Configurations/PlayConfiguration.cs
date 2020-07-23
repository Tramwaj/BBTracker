using BBTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBTrackerDBCreation.Configurations
{
    class PlayConfiguration : IEntityTypeConfiguration<Play>
    {
        public void Configure(EntityTypeBuilder<Play> builder)
        {
            builder.ToTable("Plays");
            builder.HasOne(x => x.Player)
                .WithMany(x => x.Plays)
                .IsRequired();
            builder.HasOne(x => x.Game)
                .WithMany(x => x.Plays)
                .IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PlayType).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Time).IsRequired();
            builder.Property(x => x.TeamB).IsRequired();
            
            builder.Ignore(x => x.Assister);
            builder.Ignore(x => x.Blocker);
            builder.Ignore(x => x.Fouler);
            builder.Ignore(x => x.AssistedPlayer);
            builder.Ignore(x => x.BlockedPlayer);
            builder.Ignore(x => x.FouledPlayer);
            builder.Ignore(x => x.StolenFrom);
            builder.Ignore(x => x.TurnoverCauser);

        }
    }
}
