using BBTracker.Models;
using BBTracker.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BBTracker.DB
{
    public class Serializer : IDataAccess
    {
        private BinaryFormatter _bin;
        private List<Game> _games;
        private List<Player> _players;
        private List<Play> _plays;
        private List<PlayerGame> _playerGames;    


        public Serializer()
        {
            _bin = new BinaryFormatter();
            CreateFilesIfFirstRun();
            LoadData();
        }
        private void CreateFilesIfFirstRun()
        {
            var _g = new List<Game>();
            var _py = new List<Play>();
            var _pr = new List<Player>();
            var _pg = new List<PlayerGame>();
            Serialize("games.bin", _g);
            Serialize("plays.bin", _py);
            Serialize("players.bin", _pr);
            Serialize("playergames.bin", _pg);
        }
        private void LoadData()
        {
            _games = Deserialize<List<Game>>("games.bin");
            _players = Deserialize<List<Player>>("players.bin");
            _plays = Deserialize<List<Play>>("plays.bin");
            _playerGames = Deserialize<List<PlayerGame>>("playergames.bin");
        }


        public Task<List<Game>> GetAllGamesAsync()
        {
            return Task.Run(() => { return _games; });
        }

        public Task<List<Play>> GetPlaysFromGameAsync(int gameId)
        {
            return Task.Run(() => { return _plays.Where(p => p.Game.Id == gameId).ToList(); });
        }

        public Task<List<Play>> GetPlayerPlaysAsync(int playerId)
        {
            return Task.Run(() => { return _plays.Where(p => p.Player.Id == playerId).ToList(); });
        }

        public void OpenConnection()
        {            
        }

        public void CloseConnectionAsync()
        {
        }

        public void AddGameAsync(Game game)
        {
            _games.Add(game);
        }

        public void AddPlaysAsync(ICollection<Play> plays)
        {
            _plays.AddRange(plays);
        }

        public void AddPlayerGames(ICollection<PlayerGame> playerGames)
        {
            _playerGames.AddRange(playerGames);
        }

        public void SaveChangesAsync()
        {
            Serialize("games.bin", _games);
            Serialize("plays.bin", _plays);
            Serialize("players.bin",_players);
            Serialize("playergames.bin", _playerGames);

        }

        public Task<List<Player>> GetPlayersAsyncAlreadyConnected()
        {
            return Task.Run(() => { return _players; });
        }
        private void Serialize(string file, object objToSerialize)
        {
            using (var stream =  File.Open(file, FileMode.Create))
            {
                try
                {
                    _bin.Serialize(stream, objToSerialize);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Serialization fail. Reason: " + e.Message);
                    throw;
                }
            }
        }
        
        private T Deserialize<T>(string file)
        {
            T items;

            using (Stream stream = File.Open(file, FileMode.Open))
            {
                try
                {
                    items = (T)_bin.Deserialize(stream);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("deserialization failed. Reason: " + e.Message);
                    throw;
                }
            }
            return items;
        }
    }
}
