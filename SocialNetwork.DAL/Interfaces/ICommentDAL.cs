using SocialNetwork.DTO;
using System.Collections.Generic;

namespace SocialNetwork.DAL.Interfaces
{
    public interface ICommentDAL
    {
        List<CommentDTO> GetCommentsByPostId(string postId);

        CommentDTO CreateComment(CommentDTO comment);

        CommentDTO DeleteComment(string commentId, string userId);

        CommentDTO AddLikeToComment(string commentId, string userId);

        CommentDTO RemoveLikeFromComment(string commentId, string userId);
    }
}
