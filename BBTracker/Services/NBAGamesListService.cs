using BBTracker.Models;
using Microsoft.VisualBasic;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.Services
{
    public class NBAGamesListService
    {
        private RestClient _restClient = new RestClient("https://api.sportsdata.io/v3/nba/scores/json/Games");

        public IEnumerable<NBAGame> GetGameIds(string year)
        {
            var request = new RestRequest($"season={year}")
                //.AddParameter("season", year)
                .AddParameter("key", "bd26b51e74b1458ab47c110326f242ce");
            //var req = request.Parameters.Find(param => param.Type == ParameterType.RequestBody).Value.ToString();
            var response = _restClient.Execute<IEnumerable<NBAGame>>(request);
            //_restClient.
            if (!response.IsSuccessful)
            {
                return null;
            }
            return response.Data;
        }
    }

    public class NBAGamesList
    {
        public IEnumerable<int> GameID { get; set; }
    }

    public class NBAGame
    {
        //public DateTime Start { get; set; }
        //public DateTime End { get; set; }
        public int GameID { get; set; }
        //public int Season { get; set; }
        public override string ToString()
        {
            return GameID.ToString();
        }
    }
}
