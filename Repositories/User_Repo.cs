using FlashcardAPI.Model;
using MongoDB.Driver;

namespace FlashcardAPI.Repositories
{
    public class User_Repo
    {
        private readonly IMongoCollection<User> _users;

        public User_Repo(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("users");
        }

        public async Task<User?> GetByEmailAsync(string email)
            => await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task<User?> GetByIdAsync(string id)
            => await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User user)
            => await _users.InsertOneAsync(user);
    }
}