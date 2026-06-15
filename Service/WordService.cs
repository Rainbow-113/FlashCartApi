using FlashcardAPI.Model;
using FlashcardAPI.Repositories;
using MongoDB.Driver;

namespace FlashcardAPI.Service
{
    public class WordService
    {
        private readonly Word_Repo _repo;

        public WordService(Word_Repo repo)
        {
            _repo = repo;
        }

        public async Task<List<Word>> GetWordsByFolderIdAsync(string folderId)
        {
            return await _repo.GetAll(folderId);
        }
        public async Task<Word?> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }
        public async Task CreateAsync(Word _newWord)
        {
            await _repo.Create(_newWord);
        }
        public async Task DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
        }
        //update
        public async Task UpdateAsync(string id, Word updatedWord)
        {
            await _repo.UpdateAsync( id, updatedWord);
        }
    }
}
