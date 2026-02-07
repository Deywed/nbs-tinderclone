using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services.Interfaces;
using Backend.Models;
using MongoDB.Driver;

namespace Backend.Services
{
    public class MongoUserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly ICacheService _cacheService;

        public MongoUserService(IMongoClient client, ICacheService cacheService)
        {
            var database = client.GetDatabase("TinderDb");
            _users = database.GetCollection<User>("Users");
            _cacheService = cacheService;
        }
        public Task CreateUserAsync(User user)
        {
            _cacheService.SetUserOnlineAsync(user.Id);
            return _users.InsertOneAsync(user);
        }

        public Task DeleteUserAsync(Guid id)
        {
            return _users.DeleteOneAsync(u => u.Id == id.ToString());
        }
        public Task<List<User>> GetAllUsersAsync()
        {
            return _users.Find(_ => true).ToListAsync();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public Task<User> GetUserByIdAsync(Guid id)
        {
            return _users.Find(u => u.Id == id.ToString()).FirstOrDefaultAsync();
        }
        public Task<List<User>> GetUsersByPreferencesAsync(User user)
        {
            var builder = Builders<User>.Filter;

            var filter = builder.And(
                builder.Ne(u => u.Id, user.Id),
                builder.Gte(u => u.Age, user.UserPreferences.MinAgePref),
                builder.Lte(u => u.Age, user.UserPreferences.MaxAgePref),
                builder.Eq(u => u.Gender.ToString(), user.UserPreferences.InterestedIn)
            );

            return _users.Find(filter).ToListAsync();
        }

        public Task UpdateUserAsync(User user)
        {
            _cacheService.SetUserOnlineAsync(user.Id);
            return _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }
    }
}