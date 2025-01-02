using MongoDB.Driver;
using CardsAgainstHumanity.Models;

namespace CardsAgainstHumanity.Services
{
    public class CardService
    {
        private readonly IMongoCollection<Card> _cards;

        public CardService(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _cards = database.GetCollection<Card>("Cards");

            if (_cards.CountDocuments(FilterDefinition<Card>.Empty) == 0)
            {
                var defaultCards = new List<Card>
                {
                    new Card { Id = "black1", Text = "Warum bin ich so müde?", IsBlack = true },
                    new Card { Id = "white1", Text = "Eine Tasse voll Bienen.", IsBlack = false }
                };

                _cards.InsertMany(defaultCards);
            }
        }

        public async Task<List<Card>> GetRandomCardsAsync(bool isBlack, int count)
        {
            return await _cards.Find(c => c.IsBlack == isBlack).Limit(count).ToListAsync();
        }
    }
}
