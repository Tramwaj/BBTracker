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
    public class DBAccessService : IDataAccess
    {
        public async Task<List<Game>> GetAllGamesAsync()
        {
            await using var _context = new BBTrackerContext();
            return await _context.Games.ToListAsync();
        }
        public async Task<List<Play>> GetPlaysFromGameAsync(int gameId)
        {
            await using var _context = new BBTrackerContext();
            return await _context.Plays
                .Include(p => p.Player)
                .Where(p => p.Game.Id == gameId)
                .ToListAsync();
        }

        public async Task<List<Play>> GetPlayerPlaysAsync(int playerId)
        {
            await using var _context = new BBTrackerContext();
            return await _context.Plays
                .Include(p => p.Game)
                .Where(p => p.Player.Id == playerId)
                .ToListAsync();
        }

        private BBTrackerContext _context;
        public void OpenConnection()
        {
            _context = new BBTrackerContext();
        }

        public async void CloseConnectionAsync()
        {            
            await _context.DisposeAsync();
        }

        public async void AddGameAsync(Game game)
        {
            await _context.Games
                .AddAsync(game);
        }

        public async void AddPlaysAsync(ICollection<Play> plays)
        {
            await _context.Plays
                .AddRangeAsync(plays);
        }

        public async void AddPlayerGames(ICollection<PlayerGame> playerGames)
        {
            await _context.PlayerGames
                .AddRangeAsync(playerGames);
        }
        public async Task<List<Player>> GetPlayersAsyncAlreadyConnected()
        {
            return await _context.Players.ToListAsync();
        }

        public async void SaveChangesAsync()
        {
            await _context
                .SaveChangesAsync();
        }

        //public List<Play> GetPlaysFromGame(int gameId)
        //{
        //    using var _context = new BBTrackerContext();
        //    return _context.Plays
        //        .Include(p => p.Player)
        //        .Where(p => p.Game.Id == gameId)
        //        .ToList();
        //}

    }
}
