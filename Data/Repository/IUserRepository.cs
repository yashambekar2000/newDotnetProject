using NewDotnetProject.Models;

namespace NewDotnetProject.Data.Repository
{
    public interface IUserRepository : ICollegeRepository<User>{
        Task<List<ItemDTO>> getSavedItems(int userId);
        Task<bool>SaveItems(int UserId, int ItemId);
        Task<User> checkUser(string email, string password);
          Task<bool>DeleteSavedItems(int UserId, int ItemId);
    }
}