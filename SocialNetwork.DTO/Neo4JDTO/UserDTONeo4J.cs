using Newtonsoft.Json;

namespace SocialNetwork.DTO.Neo4JDTO
{
    public class UserDTONeo4J
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
    }
}
