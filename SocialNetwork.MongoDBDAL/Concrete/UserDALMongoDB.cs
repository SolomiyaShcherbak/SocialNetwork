using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SocialNetwork.MongoDBDAL.Interfaces;
using SocialNetwork.DTO;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace SocialNetwork.MongoDBDAL.Concrete
{
    public class UserDALMongoDB : IUserDALMongoDB
    {
        string ConnectionString
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("SocialNetwork");
            }
        }

        public UserDTOMongoDB CreateUser(UserDTOMongoDB user)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTOMongoDB>("Users");

            users.InsertOne(user);

            return user;
        }

        public void DeleteUser(string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTOMongoDB>("Users");

            var filter = Builders<UserDTOMongoDB>.Filter.Eq("Id", userId);

            users.DeleteOne(filter);
        }

        public UserDTOMongoDB AuthenticateUser(string email, string password)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTOMongoDB>("Users");

            var filter = Builders<UserDTOMongoDB>.Filter.And(
                Builders<UserDTOMongoDB>.Filter.Eq("Email", email),
                Builders<UserDTOMongoDB>.Filter.Eq("Password", password));

            return users.Find(filter).FirstOrDefault();
        }

        public UserDTOMongoDB AddToSubscriptions(string userId, string personId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTOMongoDB>("Users");

            var filter = Builders<UserDTOMongoDB>.Filter.Eq("Id", userId);
            var update = Builders<UserDTOMongoDB>.Update
                .AddToSet<string>("Subscriptions", personId);

            return users.FindOneAndUpdate(filter, update);
        }

        public UserDTOMongoDB RemoveFromSubscriptions(string userId, string personId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTOMongoDB>("Users");

            var filter = Builders<UserDTOMongoDB>.Filter.Eq("Id", userId);
            var update = Builders<UserDTOMongoDB>.Update
                .Pull("Subscriptions", personId);

            return users.FindOneAndUpdate(filter, update);
        }

        public List<UserDTOMongoDB> SearchById(string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTOMongoDB>("Users");

            var filter = Builders<UserDTOMongoDB>.Filter.Eq("Id", userId);

            return users.Find(filter).ToList();
        }

        public List<UserDTOMongoDB> SearchByName(string firstName, string lastName)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTOMongoDB>("Users");

            var filter = Builders<UserDTOMongoDB>.Filter.And(
                Builders<UserDTOMongoDB>.Filter.Eq("FirstName", firstName),
                Builders<UserDTOMongoDB>.Filter.Eq("LastName", lastName));

            return users.Find(filter).ToList();
        }

        public List<UserDTOMongoDB> SearchByEmail(string email)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTOMongoDB>("Users");

            var filter = Builders<UserDTOMongoDB>.Filter.Eq("Email", email);

            return users.Find(filter).ToList();
        }


        public List<UserDTOMongoDB> SearchByInterests(string interest)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var users = database.GetCollection<UserDTOMongoDB>("Users");

            var filter = Builders<UserDTOMongoDB>.Filter.AnyEq("Interests", interest);

            return users.Find(filter).ToList();
        }
    }
}
