using SocialNetwork.DAL.Concrete;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.DTO;
using System;
using System.Collections.Generic;

namespace SocialNetwork
{
    public class ActionMenu
    {
        IPostDAL postDAL;
        ICommentDAL commentDAL;
        IUserDAL userDAL;
        UserDTO currentUser;

        public ActionMenu()
        {
            postDAL = new PostDAL();
            commentDAL = new CommentDAL();
            userDAL = new UserDAL();
        }

        protected void CreateUser()
        {
            Console.WriteLine("Enter a first name:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter a last name:");
            var lastName = Console.ReadLine();
            Console.WriteLine("Enter a sex: M/F/O");
            var sex = Console.ReadLine();
            Console.WriteLine("Enter email:");
            var email = Console.ReadLine();
            Console.WriteLine("Enter password:");
            var password = Console.ReadLine();

            Console.WriteLine("Enter your interests (press 0 to finish)");
            var interest = Console.ReadLine();
            var interests = new List<string>();
            while (interest != "0")
            {
                interests.Add(interest);
                interest = Console.ReadLine();
            }

            var user = new UserDTO
            {
                FirstName = firstName,
                LastName = lastName,
                Sex = sex,
                Email = email,
                Password = password,
                Interests = interests,
                Subscriptions = new List<string>()
            };
            user = userDAL.CreateUser(user);
            Console.WriteLine($"New user Id: {user.Id}");
        }

        protected void AuthenticateUser(ref bool status)
        {
            ConsoleKeyInfo key;
            string password = "";
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter password:");
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace)
                {
                    password += key.KeyChar;

                    Console.Write("*");
                }
                else
                {
                    if (password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            password = password.Replace("\r", "");

            currentUser = userDAL.AuthenticateUser(email, password);
            if (currentUser != null)
                status = true;
            else
                Console.WriteLine("\nInvalid email or password");
        }

        protected void ShowStream()
        {
            var stream = postDAL.GetStream();
            var comments = new List<CommentDTO>();
            displayPostsAndComments(stream, comments);
        }

        protected void CreatePost()
        {
            Console.WriteLine("Enter post title:");
            var title = Console.ReadLine();
            Console.WriteLine("Enter post body:");
            var body = Console.ReadLine();

            var post = new PostDTO
            {
                UserId = currentUser.Id,
                User = currentUser.FirstName + " " + currentUser.LastName,
                Title = title,
                Body = body,
                CreatedDate = DateTime.UtcNow.ToString(),
                LikeCount = 0,
                Likes = new List<string>()
            };
            post = postDAL.CreatePost(post);
            Console.WriteLine($"New post Id: {post.Id}");
        }

        protected void DeletePost()
        {
            Console.WriteLine("Enter post Id:");
            var postId = Console.ReadLine();
            postDAL.DeletePost(postId, currentUser.Id);
        }

        protected void AddCommentToPost()
        {
            Console.WriteLine("Enter post Id:");
            var postId = Console.ReadLine();
            Console.WriteLine("Enter comment body:");
            var body = Console.ReadLine();

            var comment = new CommentDTO
            {
                Body = body,
                PostId = postId,
                UserId = currentUser.Id,
                User = currentUser.FirstName + " " + currentUser.LastName,
                CreatedDate = DateTime.UtcNow.ToString(),
                LikeCount = 0,
                Likes = new List<string>()
            };
            comment = commentDAL.CreateComment(comment);
            Console.WriteLine($"New comment Id: {comment.Id}");
        }

        protected void RemoveCommentFromPost()
        {
            Console.WriteLine("Enter comment Id:");
            var commentId = Console.ReadLine();
            commentDAL.DeleteComment(commentId, currentUser.Id);
        }

        protected void AddLikeToPost()
        {
            Console.WriteLine("Enter post Id:");
            var postId = Console.ReadLine();
            postDAL.AddLikeToPost(postId, currentUser.Id);
        }

        protected void RemoveLikeFromPost()
        {
            Console.WriteLine("Enter post Id:");
            var postId = Console.ReadLine();
            postDAL.RemoveLikeFromPost(postId, currentUser.Id);
        }

        protected void AddLikeToComment()
        {
            Console.WriteLine("Enter comment Id:");
            var commentId = Console.ReadLine();
            commentDAL.AddLikeToComment(commentId, currentUser.Id);
        }

        protected void RemoveLikeFromComment()
        {
            Console.WriteLine("Enter comment Id:");
            var commentId = Console.ReadLine();
            commentDAL.RemoveLikeFromComment(commentId, currentUser.Id);
        }

        protected void ShowUserPosts()
        {
            Console.WriteLine("Enter user Id:");
            var userId = Console.ReadLine();
            var posts = postDAL.GetUserPosts(userId);
            var comments = new List<CommentDTO>();
            displayPostsAndComments(posts, comments);
        }

        protected void ShowAllPosts()
        {
            var posts = postDAL.GetAllPosts();
            var comments = new List<CommentDTO>();
            displayPostsAndComments(posts, comments);
        }

        protected void AddUserToSubscriptions()
        {
            Console.WriteLine("Enter user Id:");
            var personId = Console.ReadLine();
            userDAL.AddToSubscriptions(currentUser.Id, personId);
        }

        protected void RemoveUserFromSubscriptions()
        {
            Console.WriteLine("Enter user Id:");
            var personId = Console.ReadLine();
            userDAL.RemoveFromSubscriptions(currentUser.Id, personId);
        }

        protected void SearchUsersById()
        {
            Console.WriteLine("Enter user Id:");
            var userId = Console.ReadLine();
            var users = userDAL.SearchById(userId);
            displayUsers(users);
        }

        protected void SearchUsersByName()
        {
            Console.WriteLine("Enter user first name:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter user last name:");
            var lastName = Console.ReadLine();
            var users = userDAL.SearchByName(firstName, lastName);
            displayUsers(users);
        }

        protected void SearchUsersByEmail()
        {
            Console.WriteLine("Enter user email:");
            var email = Console.ReadLine();
            var users = userDAL.SearchByEmail(email);
            displayUsers(users);
        }

        protected void SearchUsersByInterests()
        {
            Console.WriteLine("Enter user interest:");
            var interest = Console.ReadLine();
            var users = userDAL.SearchByInterests(interest);
            displayUsers(users);
        }

        void displayPostsAndComments(List<PostDTO> posts, List<CommentDTO> comments)
        {
            Console.WriteLine("\nPosts:");
            foreach (var post in posts)
            {
                Console.WriteLine(post.ToString());
                comments = commentDAL.GetCommentsByPostId(post.Id);
                Console.WriteLine("\n\tComments:");
                foreach (var comment in comments)
                {
                    Console.WriteLine(comment.ToString());
                }
            }
        }

        void displayUsers(List<UserDTO> users)
        {
            Console.WriteLine("\nUsers:");
            foreach(var user in users)
            {
                Console.WriteLine(user.ToString());
            }
        }
    }
}
