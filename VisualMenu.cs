using System;

namespace SocialNetwork
{
    public class VisualMenu
    {
        public void ShowStartMenu()
        {
            string menu = @"
Social network:
1: Log in ->
2: Sign up ->
0: Exit";
            Console.WriteLine(menu);
        }

        public void ShowMainMenu()
        {
            string menu = @"
Move to category:
1: Posts ->
2: Users ->
3: Show a stream ->
4: Delete your account ->
0: Back";
            Console.WriteLine(menu);
        }

        public void ShowPostsMenu()
        {
            string menu = @"
Posts menu
1: Create a post ->
2: Delete a post
3: Show user's posts ->
4: Show all posts ->
0: Back";
            Console.WriteLine(menu);
        }

        public void ShowUsersMenu()
        {
            string menu = @"
Users menu
1: Search users ->
2: Add/remove a user from subscriptions
0: Back";
            Console.WriteLine(menu);
        }

        public void ShowSearchUsersMenu()
        {
            string menu = @"
Search users menu
1: Search users by Id
2: Search users by name
3: Search users by email
4: Search users by interests
0: Back";
            Console.WriteLine(menu);
        }

        public void ShowCommentsAndLikesToPostMenu()
        {
            string menu = @"
Comments and likes to a post menu
1: Add a comment to a post
2: Remove a comment from a post
3: Add a like to a post
4: Remove a like from a post
0: Back";
            Console.WriteLine(menu);
        }

        public void ShowLikesToCommentMenu()
        {
            string menu = @"
Likes to comment menu
1: Add a like to a comment
2: Remove a like from a comment
0: Back";
            Console.WriteLine(menu);
        }

        public void ShowManageSubscriptionsMenu()
        {
            string menu = @"
Manage subscriptions menu
1: Add a user to subscriptions
2: Remove a user from subscriptions
0: Back";
            Console.WriteLine(menu);
        }
    }
}
