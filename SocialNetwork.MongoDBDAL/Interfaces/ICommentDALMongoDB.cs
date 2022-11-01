using SocialNetwork.DTO;
using System.Collections.Generic;

namespace SocialNetwork.MongoDBDAL.Interfaces
{
    public interface ICommentDALMongoDB
    {
        List<CommentDTOMongoDB> GetCommentsByPostId(string postId);

        CommentDTOMongoDB CreateComment(CommentDTOMongoDB comment);

        CommentDTOMongoDB DeleteComment(string commentId, string userId);

        CommentDTOMongoDB AddLikeToComment(string commentId, string userId);

        CommentDTOMongoDB RemoveLikeFromComment(string commentId, string userId);
    }
}
