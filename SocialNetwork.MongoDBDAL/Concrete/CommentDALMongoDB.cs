using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SocialNetwork.DTO;
using SocialNetwork.MongoDBDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.MongoDBDAL.Concrete
{
    public class CommentDALMongoDB : ICommentDALMongoDB
    {
        string ConnectionString
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("SocialNetwork");
            }
        }

        public List<CommentDTOMongoDB> GetCommentsByPostId(string postId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var comments = database.GetCollection<CommentDTOMongoDB>("Comments");

            var filter = Builders<CommentDTOMongoDB>.Filter.Eq("PostId", postId);

            return comments.Find(filter).ToList();
        }

        public CommentDTOMongoDB CreateComment(CommentDTOMongoDB comment)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var comments = database.GetCollection<CommentDTOMongoDB>("Comments");

            comments.InsertOne(comment);

            return comment;
        }

        public CommentDTOMongoDB DeleteComment(string commentId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var comments = database.GetCollection<CommentDTOMongoDB>("Comments");

            var filter = Builders<CommentDTOMongoDB>.Filter.And(
                Builders<CommentDTOMongoDB>.Filter.Eq("Id", commentId),
                Builders<CommentDTOMongoDB>.Filter.Eq("UserId", userId));

            comments.DeleteOne(filter);

            return null;
        }

        public CommentDTOMongoDB AddLikeToComment(string commentId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var comments = database.GetCollection<CommentDTOMongoDB>("Comments");

            var filter = Builders<CommentDTOMongoDB>.Filter.Eq("Id", commentId);
            var update = Builders<CommentDTOMongoDB>.Update
                .AddToSet<string>("Likes", userId)
                .Inc("LikeCount", 1);

            return comments.FindOneAndUpdate(filter, update);
        }

        public CommentDTOMongoDB RemoveLikeFromComment(string commentId, string userId)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("SocialNetwork");
            var comments = database.GetCollection<CommentDTOMongoDB>("Comments");

            var filter = Builders<CommentDTOMongoDB>.Filter.Eq("Id", commentId);
            var update = Builders<CommentDTOMongoDB>.Update
                .Pull("Likes", userId)
                .Inc("LikeCount", -1);

            return comments.FindOneAndUpdate(filter, update);
        }
    }
}
