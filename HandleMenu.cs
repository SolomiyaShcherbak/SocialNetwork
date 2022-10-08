using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork
{
    public class HandleMenu : ActionMenu
    {
        VisualMenu visualMenu;

        public HandleMenu()
        {
            visualMenu = new VisualMenu();
        }

        public void handleInitialMenu()
        {
            string userInput = "";
            do
            {
                visualMenu.ShowStartMenu();
                userInput = Console.ReadLine();
                handleStartMenu(userInput);
            }
            while (userInput != "0");
        }

        void handleStartMenu(string userInput)
        {
            Console.Clear();
            bool status = false;
            switch (userInput)
            {
                case "1":
                    do
                    {
                        AuthenticateUser(ref status);
                    } while (status != true);
                    do
                    {
                        visualMenu.ShowMainMenu();
                        userInput = Console.ReadLine();
                        handleMainMenu(userInput);
                    } while (userInput != "0");
                    break;
                case "2":
                    CreateUser();
                    userInput = "1";
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid option\n");
                    break;
            }
        }

        void handleMainMenu(string userInput)
        {
            Console.Clear();
            switch (userInput)
            {
                case "1":
                    do
                    {
                        visualMenu.ShowPostsMenu();
                        userInput = Console.ReadLine();
                        handlePostsMenu(userInput);
                    } while (userInput != "0");
                    break;
                case "2":
                    do
                    {
                        visualMenu.ShowUsersMenu();
                        userInput = Console.ReadLine();
                        handleUsersMenu(userInput);
                    } while (userInput != "0");
                    break;
                case "3":
                    ShowStream();
                    invokeCommentsAndLikesToPostMenu(ref userInput);
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid option\n");
                    break;
            }
        }

        void handlePostsMenu(string userInput)
        {
            Console.Clear();
            switch (userInput)
            {
                case "1":
                    CreatePost();
                    invokeCommentsAndLikesToPostMenu(ref userInput);
                    break;
                case "2":
                    DeletePost();
                    break;
                case "3":
                    ShowUserPosts();
                    invokeCommentsAndLikesToPostMenu(ref userInput);
                    break;
                case "4":
                    ShowAllPosts();
                    invokeCommentsAndLikesToPostMenu(ref userInput);
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid option\n");
                    break;
            }
        }

        void handleCommentsAndLikesToPostMenu(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    AddCommentToPost();
                    do
                    {
                        visualMenu.ShowLikesToCommentMenu();
                        userInput = Console.ReadLine();
                        handleLikesToCommentMenu(userInput);
                    } while (userInput != "0");
                    break;
                case "2":
                    RemoveCommentFromPost();
                    break;
                case "3":
                    AddLikeToPost();
                    break;
                case "4":
                    RemoveLikeFromPost();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid option\n");
                    break;
            }
        }

        void handleLikesToCommentMenu(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    AddLikeToComment();
                    break;
                case "2":
                    RemoveLikeFromComment();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid option\n");
                    break;
            }
        }

        void handleUsersMenu(string userInput)
        {
            Console.Clear();
            switch (userInput)
            {
                case "1":
                    do
                    {
                        visualMenu.ShowSearchUsersMenu();
                        userInput = Console.ReadLine();
                        handleSearchUsersMenu(userInput);
                    } while (userInput != "0");
                    break;
                case "2":
                    invokeManageSubscriptionsMenu(ref userInput);
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid option\n");
                    break;
            }
        }

        void handleSearchUsersMenu(string userInput)
        {
            Console.Clear();
            switch (userInput)
            {
                case "1":
                    SearchUsersById();
                    invokeManageSubscriptionsMenu(ref userInput);
                    break;
                case "2":
                    SearchUsersByName();
                    invokeManageSubscriptionsMenu(ref userInput);
                    break;
                case "3":
                    SearchUsersByEmail();
                    invokeManageSubscriptionsMenu(ref userInput);
                    break;
                case "4":
                    SearchUsersByInterests();
                    invokeManageSubscriptionsMenu(ref userInput);
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid option\n");
                    break;
            }
        }

        void handleManageSubscriptionsMenu(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    AddUserToSubscriptions();
                    break;
                case "2":
                    RemoveUserFromSubscriptions();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid option\n");
                    break;
            }
        }

        void invokeCommentsAndLikesToPostMenu(ref string userInput)
        {
            do
            {
                visualMenu.ShowCommentsAndLikesToPostMenu();
                userInput = Console.ReadLine();
                handleCommentsAndLikesToPostMenu(userInput);
            } while (userInput != "0");
        }

        void invokeManageSubscriptionsMenu(ref string userInput)
        {
            do
            {
                visualMenu.ShowManageSubscriptionsMenu();
                userInput = Console.ReadLine();
                handleManageSubscriptionsMenu(userInput);
            } while (userInput != "0");
        }
    }
}
