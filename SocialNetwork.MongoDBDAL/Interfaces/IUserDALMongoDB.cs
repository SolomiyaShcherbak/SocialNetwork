using SocialNetwork.DTO;
using System.Collections.Generic;

namespace SocialNetwork.MongoDBDAL.Interfaces
{
    public interface IUserDALMongoDB
    {
        UserDTOMongoDB CreateUser(UserDTOMongoDB user);

        void DeleteUser(string userId);

        UserDTOMongoDB AuthenticateUser(string email, string password);

        UserDTOMongoDB AddToSubscriptions(string userId, string personId);

        UserDTOMongoDB RemoveFromSubscriptions(string userId, string personId);

        List<UserDTOMongoDB> SearchById(string userId);

        List<UserDTOMongoDB> SearchByName(string firstName, string lastName);

        List<UserDTOMongoDB> SearchByEmail(string email);

        List<UserDTOMongoDB> SearchByInterests(string interest);
    }
}
