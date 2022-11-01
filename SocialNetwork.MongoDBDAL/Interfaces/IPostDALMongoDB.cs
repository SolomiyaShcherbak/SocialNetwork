using SocialNetwork.DTO;
using System.Collections.Generic;

namespace SocialNetwork.MongoDBDAL.Interfaces
{
    public interface IPostDALMongoDB
    {
        List<PostDTOMongoDB> GetStream();

        List<PostDTOMongoDB> GetAllPosts();

        List<PostDTOMongoDB> GetUserPosts(string userId);

        PostDTOMongoDB CreatePost(PostDTOMongoDB post);

        PostDTOMongoDB DeletePost(string postId, string userId);

        PostDTOMongoDB AddLikeToPost(string postId, string userId);

        PostDTOMongoDB RemoveLikeFromPost(string postId, string userId);
    }
}
