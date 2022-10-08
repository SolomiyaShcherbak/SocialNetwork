using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
