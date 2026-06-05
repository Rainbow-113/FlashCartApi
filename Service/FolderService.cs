using FlashcardAPI.Model;
using MongoDB.Driver;

namespace FlashcardAPI.Service
{
    public class FolderService
    {
        private readonly IMongoCollection<Folder> _folder;
        public FolderService(IMongoDatabase database)
        {
            _folder = database.GetCollection<Folder>("folders");
        }
        public async Task<List<Folder>> GetAllAsync()
        {
            return await _folder.Find(folder => true).ToListAsync();
        }

        public async Task CreatceAsync(Folder newFolder)
        {
            await _folder.InsertOneAsync(newFolder);
        }
    }
}
