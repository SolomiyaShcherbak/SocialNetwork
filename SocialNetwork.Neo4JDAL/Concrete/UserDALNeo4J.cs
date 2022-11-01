using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.ApplicationServices;
using Neo4jClient;
using Neo4jClient.Cypher;
using SocialNetwork.DTO.Neo4JDTO;
using SocialNetwork.Neo4JDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Neo4JDAL.Concrete
{
    public class UserDALNeo4J : IUserDALNeo4J
    {
        string URL
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings1.json").Build().GetSection("AppSettings")["URL"];
            }
        }

        string Username
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings1.json").Build().GetSection("AppSettings")["Username"];
            }
        }

        string Password
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings1.json").Build().GetSection("AppSettings")["Password"];
            }
        }

        public UserDTONeo4J CreateUserNode(UserDTONeo4J user)
        {
            var client = new BoltGraphClient(URL, Username, Password);
            client.ConnectAsync().Wait();
            client.Cypher
                .Create("(user:User $newUser)")
                .WithParam("newUser", user)
                .ExecuteWithoutResultsAsync().Wait();
            return user;
        }

        public UserDTONeo4J DeleteUserNode(string userId)
        {
            var client = new BoltGraphClient(URL, Username, Password);
            client.ConnectAsync().Wait();
            DeleteAllRelationships(userId);
            client.Cypher
                .Match("(user:User {Id: $Id})")
                .WithParam("Id", userId)
                .Delete("user")
                .ExecuteWithoutResultsAsync().Wait();
            return null;
        }

        public void CreateRelationship(string user1Id, string user2Id)
        {
            var client = new BoltGraphClient(URL, Username, Password);
            client.ConnectAsync().Wait();
            client.Cypher
                .Match("(user1:User {Id: $Id1})", "(user2:User {Id: $Id2})")
                .WithParam("Id1", user1Id)
                .WithParam("Id2", user2Id)
                .Create("(user1)-[:SUBSCRIBED_TO]->(user2)")
                .ExecuteWithoutResultsAsync().Wait();
        }

        public void DeleteRelationship(string user1Id, string user2Id)
        {
            var client = new BoltGraphClient(URL, Username, Password);
            client.ConnectAsync().Wait();
            client.Cypher
                .Match("(user1:User {Id: $Id1})-[r:SUBSCRIBED_TO]->(user2:User {Id: $Id2})")
                .WithParam("Id1", user1Id)
                .WithParam("Id2", user2Id)
                .Delete("r")
                .ExecuteWithoutResultsAsync().Wait();
        }

        public void DeleteAllRelationships(string userId)
        {
            var client = new BoltGraphClient(URL, Username, Password);
            client.ConnectAsync().Wait();
            client.Cypher
                .Match("(user1:User {Id: $Id})-[r:SUBSCRIBED_TO]->(user2:User)")
                .WithParam("Id", userId)
                .Delete("r")
                .ExecuteWithoutResultsAsync().Wait();
        }

        public UserDTONeo4J FindRelationship(string userId, string subscribedId)
        {
            var client = new BoltGraphClient(URL, Username, Password);
            client.ConnectAsync().Wait();
            var result = client.Cypher
                .Match("(user1:User {Id: $Id1})-[r:SUBSCRIBED_TO]->(user2:User {Id: $Id2})")
                .WithParam("Id1", userId)
                .WithParam("Id2", subscribedId)
                .Return(user2 => user2.As<UserDTONeo4J>())
                .ResultsAsync.Result.FirstOrDefault();
            return result;
        }

        public int FindShortestPath(string user1Id, string user2Id)
        {
            var client = new BoltGraphClient(URL, Username, Password);
            client.ConnectAsync().Wait();
            var shortestPathValue =
                client.Cypher
                .Match("p = shortestPath((user1:User)-[*..5]-(user2:User))")
                .Where("user1.Id = $Id1")
                .WithParam("Id1", user1Id)
                .AndWhere("user2.Id = $Id2")
                .WithParam("Id2", user2Id)
                .Return(p => Return.As<int>("length(p)"))
                .ResultsAsync.Result.FirstOrDefault();
            return shortestPathValue;
        }
    }
}
