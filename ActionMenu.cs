using SocialNetwork.MongoDBDAL.Concrete;
<<<<<<< HEAD
using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using SocialNetwork.BLL;
using SocialNetwork.Neo4JDAL.Concrete;
=======
using SocialNetwork.MongoDBDAL.Interfaces;
using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using SocialNetwork.BLL;
using SocialNetwork.Neo4JDAL.Concrete;
using SocialNetwork.DTO.Neo4JDTO;
>>>>>>> 095759200502d0442076f1296f8cbea7ce452052

namespace SocialNetwork
{
    public class ActionMenu
    {
        Command command;
        UserDTOMongoDB currentUser;

        public ActionMenu()
        {
            command = new Command(new UserDALMongoDB(), new PostDALMongoDB(), new CommentDALMongoDB(), new UserDALNeo4J());
        }

        public void CreateUser()
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

            var user = new UserDTOMongoDB
            {
                FirstName = firstName,
                LastName = lastName,
                Sex = sex,
                Email = email,
                Password = password,
                Interests = interests,
                Subscriptions = new List<string>()
            };
            user = command.CreateUser(user);
            Console.WriteLine($"New user Id: {user.Id}");
        }

        public void DeleteUser()
        {
            command.DeleteUser(currentUser.Id);
        }

        public void AuthenticateUser(ref bool status)
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

            currentUser = command.AuthenticateUser(email, password);
            if (currentUser != null)
                status = true;
            else
                Console.WriteLine("\nInvalid email or password");
        }

        protected void ShowStream()
        {
            var stream = command.GetStream();
            var comments = new List<CommentDTOMongoDB>();
            displayPostsAndComments(stream, comments);
        }

        public void CreatePost()
        {
            Console.WriteLine("Enter post title:");
            var title = Console.ReadLine();
            Console.WriteLine("Enter post body:");
            var body = Console.ReadLine();

            var post = new PostDTOMongoDB
            {
                UserId = currentUser.Id,
                User = currentUser.FirstName + " " + currentUser.LastName,
                Title = title,
                Body = body,
                CreatedDate = DateTime.UtcNow.ToString(),
                LikeCount = 0,
                Likes = new List<string>()
            };
            post = command.CreatePost(post);
            Console.WriteLine($"New post Id: {post.Id}");
        }

        public void DeletePost()
        {
            Console.WriteLine("Enter post Id:");
            var postId = Console.ReadLine();
            command.DeletePost(postId, currentUser.Id);
        }

        public void AddCommentToPost()
        {
            Console.WriteLine("Enter post Id:");
            var postId = Console.ReadLine();
            Console.WriteLine("Enter comment body:");
            var body = Console.ReadLine();

            var comment = new CommentDTOMongoDB
            {
                Body = body,
                PostId = postId,
                UserId = currentUser.Id,
                User = currentUser.FirstName + " " + currentUser.LastName,
                CreatedDate = DateTime.UtcNow.ToString(),
                LikeCount = 0,
                Likes = new List<string>()
            };
            comment = command.AddCommentToPost(comment);
            Console.WriteLine($"New comment Id: {comment.Id}");
        }

        public void RemoveCommentFromPost()
        {
            Console.WriteLine("Enter comment Id:");
            var commentId = Console.ReadLine();
            command.RemoveCommentFromPost(commentId, currentUser.Id);
        }

        public void AddLikeToPost()
        {
            Console.WriteLine("Enter post Id:");
            var postId = Console.ReadLine();
            command.AddLikeToPost(postId, currentUser.Id);
        }

        public void RemoveLikeFromPost()
        {
            Console.WriteLine("Enter post Id:");
            var postId = Console.ReadLine();
            command.RemoveLikeFromPost(postId, currentUser.Id);
        }

        public void AddLikeToComment()
        {
            Console.WriteLine("Enter comment Id:");
            var commentId = Console.ReadLine();
            command.AddLikeToComment(commentId, currentUser.Id);
        }

        public void RemoveLikeFromComment()
        {
            Console.WriteLine("Enter comment Id:");
            var commentId = Console.ReadLine();
            command.RemoveLikeFromComment(commentId, currentUser.Id);
        }

        public void ShowUserPosts()
        {
            Console.WriteLine("Enter user Id:");
            var userId = Console.ReadLine();
            var posts = command.GetUserPosts(userId);
            var comments = new List<CommentDTOMongoDB>();
            displayPostsAndComments(posts, comments);
        }

        public void ShowAllPosts()
        {
            var posts = command.GetAllPosts();
            var comments = new List<CommentDTOMongoDB>();
            displayPostsAndComments(posts, comments);
        }

        public void AddUserToSubscriptions()
        {
            Console.WriteLine("Enter user Id:");
            var personId = Console.ReadLine();
            command.AddUserToSubscriptions(currentUser.Id, personId);
        }

        public void RemoveUserFromSubscriptions()
        {
            Console.WriteLine("Enter user Id:");
            var personId = Console.ReadLine();
            command.RemoveUserFromSubscriptions(currentUser.Id, personId);
        }

        public void SearchUsersById()
        {
            Console.WriteLine("Enter user Id:");
            var userId = Console.ReadLine();
            var users = command.SearchUsersById(userId);
            displayUsers(users);
        }

        public void SearchUsersByName()
        {
            Console.WriteLine("Enter user first name:");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter user last name:");
            var lastName = Console.ReadLine();
            var users = command.SearchUsersByName(firstName, lastName);
            displayUsers(users);
        }

        public void SearchUsersByEmail()
        {
            Console.WriteLine("Enter user email:");
            var email = Console.ReadLine();
            var users = command.SearchUsersByEmail(email);
            displayUsers(users);
        }

        public void SearchUsersByInterests()
        {
            Console.WriteLine("Enter user interest:");
            var interest = Console.ReadLine();
            var users = command.SearchUsersByInterests(interest);
            displayUsers(users);
        }

        void displayUsers(List<UserDTOMongoDB> users)
        {
            Console.WriteLine("\nUsers:");
            foreach (var user in users)
            {
                Console.WriteLine(user.ToString());
                displayConnectionIfExists(currentUser.Id, user.Id);
                displayShortestPath(currentUser.Id, user.Id);
            }
        }

        void displayPostsAndComments(List<PostDTOMongoDB> posts, List<CommentDTOMongoDB> comments)
        {
            Console.WriteLine("\nPosts:");
            foreach (var post in posts)
            {
                Console.WriteLine(post.ToString());
                comments = command.GetCommentsByPostId(post.Id);
                Console.WriteLine("\n\tComments:");
                foreach (var comment in comments)
                {
                    Console.WriteLine(comment.ToString());
                }
            }
        }

        void displayConnectionIfExists(string userId, string personId)
        {
            if (command.CheckRelationship(userId, personId) != null)
                Console.WriteLine("You are subscribed to this user");
            else
                Console.WriteLine("You are not subscribed to this user");
        }

        void displayShortestPath(string userId, string personId)
        {
            Console.WriteLine($"Shortest path to user: {command.FindShortestPath(userId, personId)}");
        }
    }
}
