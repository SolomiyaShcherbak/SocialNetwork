using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO.Neo4JDTO
{
    public class UserDTONeo4J
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
    }
}
