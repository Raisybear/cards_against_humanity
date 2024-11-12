using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace cards_against_humanity_backend.Models
{
    public class Item
    {
        [BsonId]  // Kennzeichnet das Feld als Primärschlüssel
        [BsonRepresentation(BsonType.ObjectId)]  // Gibt an, dass dieses Feld als MongoDB-ObjectId behandelt wird
        public string Id { get; set; }  // Id ist vom Typ string und kann null sein

        [BsonElement("name")]
        public string Name { get; set; }
    }
}