using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services.Interfaces;
using Backend.Models;
using MongoDB.Driver;

namespace Backend.Services
{
    public class MongoUserService : IMongoUserService
    {
        private readonly IMongoCollection<User> _users;

        public MongoUserService(IMongoClient client)
        {
            var database = client.GetDatabase("TinderDb");
            _users = database.GetCollection<User>("Users");
        }
        public Task CreateUserAsync(User user) =>
            _users.InsertOneAsync(user);

        public Task DeleteUserAsync(Guid id) =>
        null!;
        // _users.DeleteOneAsync(u => u.Id == id);
        public Task<List<User>> GetAllUsersAsync()
        {
            return _users.Find(_ => true).ToListAsync();
        }

        public Task<User> GetUserByEmailAsync(string email) =>
            _users.Find(u => u.Email == email).FirstOrDefaultAsync();

        public Task<User> GetUserByIdAsync(Guid id) =>
        null!;
        // _users.Find(u => u.Id == id).FirstOrDefaultAsync();

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

        public Task UpdateUserAsync(User user) =>
            _users.ReplaceOneAsync(u => u.Id == user.Id, user);
    }
}