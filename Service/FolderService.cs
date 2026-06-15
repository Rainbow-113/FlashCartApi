using FlashcardAPI.Model;
using FlashcardAPI.Repositories;
using MongoDB.Driver;

namespace FlashcardAPI.Service
{
    public class FolderService
    {
        private readonly Folder_Repo _repo;
        public FolderService(Folder_Repo repo)
        {
            _repo = repo;
        }
        public async Task<List<Folder>> GetAllAsync(string userId)
        {
            return await _repo.GetAll(userId);
        }
        public async Task<Folder?> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public async Task CreatceAsync(Folder newFolder, string userId)
        {
            var existing = await _repo.GetByTitleAsync(newFolder.Title);
            if (existing != null)
                throw new Exception("Tên folder đã tồn tại!");
            newFolder.UserId = userId;
            await _repo.Create(newFolder);
        }

        public async Task DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
        }
        //update
        public async Task UpdateAsync(string id, Folder updatedFolder)
        {
            // Lấy folder hiện tại
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) throw new Exception("Không tìm thấy folder!");

            // Chỉ cho phép sửa Title và Description
            existing.Title = updatedFolder.Title;
            existing.Description = updatedFolder.Description;

            // Giữ nguyên Id, UserId, TotalWords, MaxWords, CreatedAt
            await _repo.UpdateAsync(id, existing);
        }
        //search
        public async Task<List<Folder>> SearchAsync(string userId, string keyword)
        {
            return await _repo.SearchAsync(userId, keyword);
        }
    }
}
