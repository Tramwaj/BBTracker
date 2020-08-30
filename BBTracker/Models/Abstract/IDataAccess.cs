using BBTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBTracker.Services
{
    public interface IDataAccess
    {
        Task<List<Game>> GetAllGamesAsync();
        Task<List<Play>> GetPlaysFromGameAsync(int gameId);
        List<Play> GetPlaysFromGame(int gameId);
        List<Play> GetPlayerPlays(int playerId);
        //void start

    }
}
