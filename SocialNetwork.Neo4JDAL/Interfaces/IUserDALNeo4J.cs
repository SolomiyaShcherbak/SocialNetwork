using SocialNetwork.DTO.Neo4JDTO;

namespace SocialNetwork.Neo4JDAL.Interfaces
{
    public interface IUserDALNeo4J
    {
        UserDTONeo4J CreateUserNode(UserDTONeo4J user);

        UserDTONeo4J DeleteUserNode(string id);

        void CreateRelationship(string user1Id, string user2Id);

        void DeleteRelationship(string user1Id, string user2Id);

        void DeleteAllRelationships(string userId);

        UserDTONeo4J FindRelationship(string userId, string subscribedId);

        int FindShortestPath(string user1Id, string user2Id);
    }
}
