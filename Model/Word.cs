using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlashcardAPI.Model
{
    public class Word
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string FolderId { get; set; } = null;
        [BsonElement("English")]
        public string English { get; set; } = null!;
        [BsonElement("ExampleEnglish")]
        public string? ExampleEnglish { get; set; } // Ví dụ: "I eat an apple"
        [BsonElement("Vietnamese")]
        public string Vietnamese { get; set; } = null!;
        [BsonElement("ExampleVietnamese")]
        public string? ExampleVietnamese { get; set; } // Nghĩa VD: "Tôi ăn một quả táo"
        [BsonElement("Interval")]
        public string Interval { get; set; } = "3 giờ";
        [BsonElement("IsDueToday")]
        public bool IsDueToday { get; set; } = true;
        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
