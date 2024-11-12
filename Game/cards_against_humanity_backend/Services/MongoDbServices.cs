using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using cards_against_humanity_backend.Models;
using Microsoft.Extensions.Options;

namespace cards_against_humanity_backend.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Item> _itemsCollection;

        public MongoDbService(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _itemsCollection = database.GetCollection<Item>(settings.Value.CollectionName);
        }

        // Alle Items abrufen
        public async Task<List<Item>> GetAllAsync()
        {
            return await _itemsCollection.Find(_ => true).ToListAsync();
        }

        // Ein einzelnes Item anhand der ID abrufen
        public async Task<Item> GetAsync(string id)
        {
            return await _itemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        // Ein neues Item erstellen
        public async Task CreateAsync(Item newItem)
        {
            await _itemsCollection.InsertOneAsync(newItem);
        }

        // Ein Item anhand der ID aktualisieren
        public async Task UpdateAsync(string id, Item updatedItem)
        {
            var objectId = new ObjectId(id); // Konvertiere den String in ObjectId
            await _itemsCollection.ReplaceOneAsync(item => item.Id == objectId.ToString(), updatedItem);
        }

        // Ein Item anhand der ID löschen
        public async Task DeleteAsync(string id)
        {
            var objectId = new ObjectId(id); // Konvertiere den String in ObjectId
            await _itemsCollection.DeleteOneAsync(item => item.Id == objectId.ToString());
        }
    }
}