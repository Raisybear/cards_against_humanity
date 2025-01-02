using MongoDB.Driver;
using CardsAgainstHumanity.Models;

namespace CardsAgainstHumanity.Services
{
    public class GameService
    {
        private readonly IMongoCollection<Game> _games;

        public GameService(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _games = database.GetCollection<Game>("Games");
        }

        public async Task<Game> CreateGameAsync()
        {
            var newGame = new Game { Id = Guid.NewGuid().ToString() };
            await _games.InsertOneAsync(newGame);
            return newGame;
        }

        public async Task<Game> GetGameByIdAsync(string id)
        {
            return await _games.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateGameAsync(Game game)
        {
            await _games.ReplaceOneAsync(g => g.Id == game.Id, game);
        }
    }
}
