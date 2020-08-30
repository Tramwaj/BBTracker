using BBTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Services
{
    public class DBAccessService :IDataAccess
    {
        public async Task<List<Game>> GetAllGamesAsync()
        {
            await using var _context = new BBTrackerContext();
            return await _context.Games.ToListAsync();            
        }
        public async Task<List<Play>> GetPlaysFromGameAsync(int gameId)
        {
            await using var _context = new BBTrackerContext();
            return await _context.Plays.Where(p => p.Game.Id == gameId).ToListAsync();
        }
        public List<Play> GetPlaysFromGame(int gameId)
        {
            using var _context = new BBTrackerContext();
            return _context.Plays
                .Include(p => p.Player)
                .Where(p => p.Game.Id == gameId)                
                .ToList();
        }

        public List<Play> GetPlayerPlays(int playerId)
        {
            using var _context = new BBTrackerContext();
            return _context.Plays
                .Include(p => p.Game)
                .Where(p => p.Player.Id == playerId)
                .ToList();
        }
    }
}
