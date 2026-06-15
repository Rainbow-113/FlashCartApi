using FlashcardAPI.Model;
using MongoDB.Driver;

namespace FlashcardAPI.Repositories
{
    public class Folder_Repo
    {
        //Folder ở đấy là Model
        //khỏi tạo 1 biến chỉ sự dụng trong class (private) 
        //chỉ được gấn 1 lần biến(readonly) , kết nối với mongo và trả về model có dạng Folder 
        private readonly IMongoCollection<Folder> _folders;
        //chỉ cho repos biết collection nào trong data để lấy 
        public Folder_Repo(IMongoDatabase database)
        {
            _folders = database.GetCollection<Folder>("folders");
        }

        public async Task<List<Folder>> GetAll(string userId)
        {
            // f tượng tủng cho 1 đối tượng Trong Folders
            return await _folders.Find(f => f.UserId == userId).ToListAsync();
            //ToListAsync lấy tấy cả documen trong collection về thành dạng List<Folder>
        }
        public async Task<Folder?> GetByIdAsync(string id)
        {
            // Tìm folder có Id khớp
            return await _folders.Find(f => f.Id == id).FirstOrDefaultAsync();
        }
        public async Task Create(Folder newFolder)
        {
            await _folders.InsertOneAsync(newFolder);
        }

        public async Task DeleteAsync(string id)
        {
            await _folders.DeleteOneAsync(f => f.Id == id);
        }
        //tìm folder theo tên
        public async Task<Folder?> GetByTitleAsync(string title)
        {
            return await _folders.Find(f => f.Title == title).FirstOrDefaultAsync();
        }
        //Update
        public async Task UpdateAsync(string id, Folder updatedFolder)
        {
            await _folders.ReplaceOneAsync(f => f.Id == id, updatedFolder);
        }
        //search
        public async Task<List<Folder>> SearchAsync(string userId, string keyword)
        {
            return await _folders.Find(f =>
                f.UserId == userId &&
                f.Title.ToLower().Contains(keyword.ToLower())
            ).ToListAsync();
        }
    }
}
