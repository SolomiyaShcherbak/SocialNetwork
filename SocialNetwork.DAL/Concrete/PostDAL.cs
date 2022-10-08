using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.DAL.Concrete
{
    public class PostDAL : IPostDAL
    {
        string ConnectionString
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("SocialNetwork");
            }
        }

        public List<PostDTO> GetAllPosts()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTO>("Posts");

            return posts.Find(_ => true).ToList();
        }

        public List<PostDTO> GetStream()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTO>("Posts");

            var postsList = GetAllPosts();
            return postsList.OrderByDescending(p => DateTime.Parse(p.CreatedDate)).ToList();
        }

        public List<PostDTO> GetUserPosts(string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTO>("Posts");

            var filter = Builders<PostDTO>.Filter.Eq("UserId", userId);

            return posts.Find(filter).ToList();
        }

        public PostDTO CreatePost(PostDTO post)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTO>("Posts");

            posts.InsertOne(post);

            return post;
        }

        public PostDTO DeletePost(string postId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTO>("Posts");

            var filter = Builders<PostDTO>.Filter.And(
                Builders<PostDTO>.Filter.Eq("Id", postId),
                Builders<PostDTO>.Filter.Eq("UserId", userId));
            
            posts.DeleteOne(filter);

            return null;
        }

        public PostDTO AddLikeToPost(string postId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTO>("Posts");

            var filter = Builders<PostDTO>.Filter.Eq("Id", postId);
            var update = Builders<PostDTO>.Update
                .AddToSet<string>("Likes", userId)
                .Inc("LikeCount", 1);

            return posts.FindOneAndUpdate(filter, update);
        }

        public PostDTO RemoveLikeFromPost(string postId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var posts = database.GetCollection<PostDTO>("Posts");

            var filter = Builders<PostDTO>.Filter.Eq("Id", postId);
            var update = Builders<PostDTO>.Update
                .Pull("Likes", userId)
                .Inc("LikeCount", -1);

            return posts.FindOneAndUpdate(filter, update);
        }
    }
}
