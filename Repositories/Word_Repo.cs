    using FlashcardAPI.Model;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Driver;

    namespace FlashcardAPI.Repositories
    {
        public class Word_Repo
        {
            private readonly IMongoCollection<Word> _words;
            public Word_Repo(IMongoDatabase database)
            {
                _words = database.GetCollection<Word>("words");
            }
            //hiển thị tất cả Word theo Folder
            public async Task<List<Word>> GetAll(string folderId)
            {
                return await _words.Find(w => w.FolderId== folderId).ToListAsync();
            }
            //TÌm theo id Word
            public async Task<Word?> GetByIdAsync(string id)
            {
                return await _words.Find(w => w.Id == id).FirstOrDefaultAsync();
            }
            public async Task Create( Word _newWord)
            {
                await _words.InsertOneAsync(_newWord);
            }
            public async Task DeleteAsync(string id)
            {
                await _words.DeleteOneAsync(w => w.Id == id);
            }
            //uodate
            public async Task UpdateAsync(string id, Word updatedWord)
            {
                await _words.ReplaceOneAsync(w => w.Id == id, updatedWord);
            }

        }
    }
