using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Interfaces
{
    public interface IPostDAL
    {
        List<PostDTO> GetStream();

        List<PostDTO> GetAllPosts();

        List<PostDTO> GetUserPosts(string userId);

        PostDTO CreatePost(PostDTO post);

        PostDTO DeletePost(string postId, string userId);

        PostDTO AddLikeToPost(string postId, string userId);

        PostDTO RemoveLikeFromPost(string postId, string userId);
    }
}
