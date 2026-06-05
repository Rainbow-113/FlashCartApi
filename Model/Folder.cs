using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace FlashcardAPI.Model
{
    public class Folder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Title { get; set; } = ""; // ← Day 1, Day 2...
        public int TotalWords { get; set; } = 0; // ← số từ hiện tại
        public int MaxWords { get; set; } = 50;  // ← tối đa 50 từ
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
