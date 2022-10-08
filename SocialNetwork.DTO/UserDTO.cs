using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{
    public class UserDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Sex")]
        public string Sex { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Interests")]
        public List<string> Interests { get; set; }

        [BsonElement("Subscriptions")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Subscriptions { get; set; }

        public override string ToString()
        {
            return $"\nUser: {this.FirstName} {this.LastName}" +
                $"\nUserId: {this.Id}" +
                $"\nSex: {this.Sex}" +
                $"\nEmail: {this.Email}";
        }
    }
}
