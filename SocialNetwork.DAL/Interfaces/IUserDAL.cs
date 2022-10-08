using SocialNetwork.DTO;
using System.Collections.Generic;

namespace SocialNetwork.DAL.Interfaces
{
    public interface IUserDAL
    {
        UserDTO CreateUser(UserDTO user);

        UserDTO AuthenticateUser(string email, string password);

        UserDTO AddToSubscriptions(string userId, string personId);

        UserDTO RemoveFromSubscriptions(string userId, string personId);

        List<UserDTO> SearchById(string userId);

        List<UserDTO> SearchByName(string firstName, string lastName);

        List<UserDTO> SearchByEmail(string email);

        List<UserDTO> SearchByInterests(string interest);
    }
}
