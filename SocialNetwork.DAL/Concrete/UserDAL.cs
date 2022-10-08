using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Concrete
{
    public class UserDAL : IUserDAL
    {
        string ConnectionString
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("SocialNetwork");
            }
        }

        public UserDTO CreateUser(UserDTO user)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTO>("Users");

            users.InsertOne(user);

            return user;
        }

        public UserDTO AuthenticateUser(string email, string password)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTO>("Users");

            var filter = Builders<UserDTO>.Filter.And(
                Builders<UserDTO>.Filter.Eq("Email", email),
                Builders<UserDTO>.Filter.Eq("Password", password));

            return users.Find(filter).FirstOrDefault();
        }

        public UserDTO AddToSubscriptions(string userId, string personId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTO>("Users");

            var filter = Builders<UserDTO>.Filter.Eq("Id", userId);
            var update = Builders<UserDTO>.Update
                .AddToSet<string>("Subscriptions", personId);

            return users.FindOneAndUpdate(filter, update);
        }

        public UserDTO RemoveFromSubscriptions(string userId, string personId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTO>("Users");

            var filter = Builders<UserDTO>.Filter.Eq("Id", userId);
            var update = Builders<UserDTO>.Update
                .Pull("Subscriptions", personId);

            return users.FindOneAndUpdate(filter, update);
        }

        public List<UserDTO> SearchById(string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTO>("Users");

            var filter = Builders<UserDTO>.Filter.Eq("Id", userId);

            return users.Find(filter).ToList();
        }

        public List<UserDTO> SearchByName(string firstName, string lastName)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTO>("Users");

            var filter = Builders<UserDTO>.Filter.And(
                Builders<UserDTO>.Filter.Eq("FirstName", firstName),
                Builders<UserDTO>.Filter.Eq("LastName", lastName));

            return users.Find(filter).ToList();
        }

        public List<UserDTO> SearchByEmail(string email)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTO>("Users");

            var filter = Builders<UserDTO>.Filter.Eq("Email", email);

            return users.Find(filter).ToList();
        }


        public List<UserDTO> SearchByInterests(string interest)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTO>("Users");

            var filter = Builders<UserDTO>.Filter.AnyEq("Interests", interest);

            return users.Find(filter).ToList();
        }
    }
}
