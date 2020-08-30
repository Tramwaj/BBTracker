using BBTracker.Models;
using Microsoft.Xaml.Behaviors.Core;
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
        //List<Play> GetPlaysFromGame(int gameId);
        Task<List<Play>> GetPlayerPlaysAsync(int playerId);

        void OpenConnection();
        void CloseConnectionAsync();
        void AddGameAsync(Game game);
        void AddPlaysAsync(ICollection<Play> plays);
        void AddPlayerGames(ICollection<PlayerGame> playerGames);
        void SaveChangesAsync();
        Task<List<Player>> GetPlayersAsyncAlreadyConnected();
    }
}
