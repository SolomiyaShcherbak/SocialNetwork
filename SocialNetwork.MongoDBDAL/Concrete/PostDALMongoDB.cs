using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SocialNetwork.DTO;
using SocialNetwork.MongoDBDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.MongoDBDAL.Concrete
{
    public class PostDALMongoDB : IPostDALMongoDB
    {
        string ConnectionString
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("SocialNetwork");
            }
        }

        public List<PostDTOMongoDB> GetAllPosts()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTOMongoDB>("Posts");

            return posts.Find(_ => true).ToList();
        }

        public List<PostDTOMongoDB> GetStream()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTOMongoDB>("Posts");

            var postsList = GetAllPosts();
            return postsList.OrderByDescending(p => DateTime.Parse(p.CreatedDate)).ToList();
        }

        public List<PostDTOMongoDB> GetUserPosts(string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTOMongoDB>("Posts");

            var filter = Builders<PostDTOMongoDB>.Filter.Eq("UserId", userId);

            return posts.Find(filter).ToList();
        }

        public PostDTOMongoDB CreatePost(PostDTOMongoDB post)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTOMongoDB>("Posts");

            posts.InsertOne(post);

            return post;
        }

        public PostDTOMongoDB DeletePost(string postId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTOMongoDB>("Posts");

            var filter = Builders<PostDTOMongoDB>.Filter.And(
                Builders<PostDTOMongoDB>.Filter.Eq("Id", postId),
                Builders<PostDTOMongoDB>.Filter.Eq("UserId", userId));
            
            posts.DeleteOne(filter);

            return null;
        }

        public PostDTOMongoDB AddLikeToPost(string postId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTOMongoDB>("Posts");

            var filter = Builders<PostDTOMongoDB>.Filter.Eq("Id", postId);
            var update = Builders<PostDTOMongoDB>.Update
                .AddToSet<string>("Likes", userId)
                .Inc("LikeCount", 1);

            return posts.FindOneAndUpdate(filter, update);
        }

        public PostDTOMongoDB RemoveLikeFromPost(string postId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTOMongoDB>("Posts");

            var filter = Builders<PostDTOMongoDB>.Filter.Eq("Id", postId);
            var update = Builders<PostDTOMongoDB>.Update
                .Pull("Likes", userId)
                .Inc("LikeCount", -1);

            return posts.FindOneAndUpdate(filter, update);
        }
    }
}
