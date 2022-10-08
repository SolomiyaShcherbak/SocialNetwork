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
    public class CommentDAL : ICommentDAL
    {
        string ConnectionString
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("SocialNetwork");
            }
        }

        public List<CommentDTO> GetCommentsByPostId(string postId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var comments = database.GetCollection<CommentDTO>("Comments");

            var filter = Builders<CommentDTO>.Filter.Eq("PostId", postId);

            return comments.Find(filter).ToList();
        }

        public CommentDTO CreateComment(CommentDTO comment)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var comments = database.GetCollection<CommentDTO>("Comments");

            comments.InsertOne(comment);

            return comment;
        }

        public CommentDTO DeleteComment(string commentId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var comments = database.GetCollection<CommentDTO>("Comments");

            var filter = Builders<CommentDTO>.Filter.And(
                Builders<CommentDTO>.Filter.Eq("Id", commentId),
                Builders<CommentDTO>.Filter.Eq("UserId", userId));

            comments.DeleteOne(filter);

            return null;
        }

        public CommentDTO AddLikeToComment(string commentId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var comments = database.GetCollection<CommentDTO>("Comments");

            var filter = Builders<CommentDTO>.Filter.Eq("Id", commentId);
            var update = Builders<CommentDTO>.Update
                .AddToSet<string>("Likes", userId)
                .Inc("LikeCount", 1);

            return comments.FindOneAndUpdate(filter, update);
        }

        public CommentDTO RemoveLikeFromComment(string commentId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var comments = database.GetCollection<CommentDTO>("Comments");

            var filter = Builders<CommentDTO>.Filter.Eq("Id", commentId);
            var update = Builders<CommentDTO>.Update
                .Pull("Likes", userId)
                .Inc("LikeCount", -1);

            return comments.FindOneAndUpdate(filter, update);
        }
    }
}
