using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{
    public class CommentDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Body")]
        public string Body { get; set; }

        [BsonElement("PostId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }

        [BsonElement("UserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("User")]
        public string User { get; set; }

        [BsonElement("CreatedDate")]
        public string CreatedDate { get; set; }

        [BsonElement("LikeCount")]
        public int LikeCount { get; set; }

        [BsonElement("Likes")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Likes { get; set; }

        public override string ToString()
        {
            return $"\n\tUser: {this.User}" +
                $"\n\tUserId: {this.UserId}" +
                $"\n\tCommentId: {this.Id}" +
                $"\n\tCreatedDate: {this.CreatedDate}" +
                $"\n\tBody: \n\t{this.Body}";
        }
    }
}
