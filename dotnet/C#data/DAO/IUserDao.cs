using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        IList<User> GetUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        User CreateUser(string username, string password, string role);
        UserFavoriteDriver MakeFavoriteDriver(int driverId, string username);
        void UnselectFavoriteDriver(string username);
        int GetFavoriteDriverId(string username);
    }
}
