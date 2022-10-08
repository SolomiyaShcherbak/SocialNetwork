using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocialNetwork.DTO
{
    public class PostDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("User")]
        public string User { get; set; }

        [BsonElement("Body")]
        public string Body { get; set; }

        [BsonElement("CreatedDate")]
        public string CreatedDate { get; set; }

        [BsonElement("LikeCount")]
        public int LikeCount { get; set; }

        [BsonElement("Likes")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Likes { get; set; }

        public override string ToString()
        {
            return $"\nUser: {this.User}" +
                $"\nUserId: {this.UserId}" + 
                $"\nTitle: {this.Title}" +
                $"\nPostId: {this.Id}" +
                $"\nCreatedDate: {this.CreatedDate}" +
                $"\nBody: \n{this.Body}";
        }
    }
}
