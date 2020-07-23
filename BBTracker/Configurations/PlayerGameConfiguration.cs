using BBTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BBTracker.Configurations
{
    class PlayerGameConfiguration : IEntityTypeConfiguration<PlayerGame>
    {
        public void Configure(EntityTypeBuilder<PlayerGame> builder)
        {
            builder.ToTable("PlayerGames");
            builder.HasKey(pg => new { pg.PlayerId, pg.GameId });
            builder.HasOne(pg => pg.Player)
                .WithMany(p => p.PlayerGames)
                .HasForeignKey(pg => pg.PlayerId);
            builder.HasOne(pg => pg.Game)
                .WithMany(g => g.PlayerGames)
                .HasForeignKey(pg => pg.GameId);
            
        }
    }
}
