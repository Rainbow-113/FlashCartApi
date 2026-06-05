using FlashcardAPI.Model;
using MongoDB.Driver;

namespace FlashcardAPI.Service
{
    public class WordService
    {
        private readonly IMongoCollection<Word> _words;

        public WordService(IMongoDatabase database)
        {
            _words = database.GetCollection<Word>("words");
        }

        public async Task<List<Word>> GetWordsByFolderIdAsync(string folderId)
        {
            return await _words.Find(w => w.FolderId == folderId).ToListAsync();
        }
        public async Task CreateWordAsync(Word newWord)
        {
            // Khởi tạo các giá trị mặc định cho hệ thống lặp lại SRS
            newWord.Interval = "3 giờ";
            newWord.IsDueToday = true;

            // Đẩy thẳng dữ liệu lên MongoDB Atlas
            await _words.InsertOneAsync(newWord);
        }
    }
}
