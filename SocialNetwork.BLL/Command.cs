using SocialNetwork.DTO;
using SocialNetwork.DTO.Neo4JDTO;
using SocialNetwork.MongoDBDAL.Interfaces;
using SocialNetwork.Neo4JDAL.Interfaces;
using System.Collections.Generic;

namespace SocialNetwork.BLL
{
    public class Command
    {
        IUserDALMongoDB userDALMongoDB;
        IPostDALMongoDB postDALMongoDB;
        ICommentDALMongoDB commentDALMongoDB;
        IUserDALNeo4J userDALNeo4J;

        public Command(IUserDALMongoDB userDAL, IPostDALMongoDB postDAL, ICommentDALMongoDB commentDAL, IUserDALNeo4J userDALNeo4J)
        {
            this.userDALMongoDB = userDAL;
            this.postDALMongoDB = postDAL;
            this.commentDALMongoDB = commentDAL;
            this.userDALNeo4J = userDALNeo4J;
        }

        public UserDTOMongoDB CreateUser(UserDTOMongoDB user)
        {
            user = userDALMongoDB.CreateUser(user);
            userDALNeo4J.CreateUserNode(new UserDTONeo4J { Id = user.Id });
            return user;
        }

        public void DeleteUser(string userId)
        {
            userDALMongoDB.DeleteUser(userId);
            userDALNeo4J.DeleteUserNode(userId);
        }

        public UserDTOMongoDB AuthenticateUser(string email, string password)
        {
            return userDALMongoDB.AuthenticateUser(email, password);
        }

        public List<PostDTOMongoDB> GetStream()
        {
            return postDALMongoDB.GetStream();
        }

        public PostDTOMongoDB CreatePost(PostDTOMongoDB post)
        {
            return postDALMongoDB.CreatePost(post);
        }

        public void DeletePost(string postId, string userId)
        {
            postDALMongoDB.DeletePost(postId, userId);
        }

        public CommentDTOMongoDB AddCommentToPost(CommentDTOMongoDB comment)
        {
            return commentDALMongoDB.CreateComment(comment);
        }

        public void RemoveCommentFromPost(string commentId, string userId)
        {
            commentDALMongoDB.DeleteComment(commentId, userId);
        }

        public List<CommentDTOMongoDB> GetCommentsByPostId(string postId)
        {
            return commentDALMongoDB.GetCommentsByPostId(postId);
        }

        public void AddLikeToPost(string postId, string userId)
        {
            postDALMongoDB.AddLikeToPost(postId, userId);
        }

        public void RemoveLikeFromPost(string postId, string userId)
        {
            postDALMongoDB.RemoveLikeFromPost(postId, userId);
        }

        public void AddLikeToComment(string commentId, string userId)
        {
            commentDALMongoDB.AddLikeToComment(commentId, userId);
        }

        public void RemoveLikeFromComment(string commentId, string userId)
        {
            commentDALMongoDB.RemoveLikeFromComment(commentId, userId);
        }

        public List<PostDTOMongoDB> GetUserPosts(string userId)
        {
            return postDALMongoDB.GetUserPosts(userId);
        }

        public List<PostDTOMongoDB> GetAllPosts()
        {
            return postDALMongoDB.GetAllPosts();
        }

        public void AddUserToSubscriptions(string userId, string personId)
        {
            userDALMongoDB.AddToSubscriptions(userId, personId);
            userDALNeo4J.CreateRelationship(userId, personId);
        }

        public void RemoveUserFromSubscriptions(string userId, string personId)
        {
            userDALMongoDB.RemoveFromSubscriptions(userId, personId);
            userDALNeo4J.DeleteRelationship(userId, personId);
        }

        public UserDTONeo4J CheckConnection(string user1Id, string user2Id)
        {
            return userDALNeo4J.FindRelationship(user1Id, user2Id);
        }

        public int FindShortestPath(string user1Id, string user2Id)
        {
            return userDALNeo4J.FindShortestPath(user1Id, user2Id);
        }

        public List<UserDTOMongoDB> SearchUsersById(string userId)
        {
            return userDALMongoDB.SearchById(userId);
        }

        public List<UserDTOMongoDB> SearchUsersByName(string firstName, string lastName)
        {
            return userDALMongoDB.SearchByName(firstName, lastName);
        }

        public List<UserDTOMongoDB> SearchUsersByEmail(string email)
        {
            return userDALMongoDB.SearchByEmail(email);
        }

        public List<UserDTOMongoDB> SearchUsersByInterests(string interest)
        {
            return userDALMongoDB.SearchByInterests(interest);
        }
    }
}
