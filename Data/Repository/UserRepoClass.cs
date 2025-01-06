using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewDotnetProject.Models;

namespace NewDotnetProject.Data.Repository
{
    public class UserRepoClass : CollegeRepoClass<User> , IUserRepository{
        private readonly CollegeDBContext _DBContext;

        public UserRepoClass (CollegeDBContext DBContext) : base(DBContext){
                _DBContext = DBContext;
        }

        public async Task<List<ItemDTO>> getSavedItems(int userId){
              var savedItems = await _DBContext.SavedItems
            .Where(si => si.UserId == userId)
            .Include(si => si.Item)
             .Select(si => new ItemDTO
                {
                    Id = si.ItemId,
                    name = si.Item.name,
                      description = si.Item.description,
                        new_price = si.Item.new_price,
                         old_price = si.Item.old_price,
                          gender = si.Item.gender,
                           category = si.Item.category,
                            image_url = si.Item.image_url,
                     // Assuming Item has a Name property
                })
            .ToListAsync();
            return savedItems;
        }

        public async Task<bool> SaveItems(int UserId, int ItemId){
                   var existingSavedItem = await _DBContext.SavedItems
        .FirstOrDefaultAsync(si => si.UserId == UserId && si.ItemId == ItemId);

            if (existingSavedItem != null) {
                return false;
            }

            var savedItem = new SavedItem
            {
                UserId = UserId,
                ItemId = ItemId
            };

            await _DBContext.SavedItems.AddAsync(savedItem);
            await _DBContext.SaveChangesAsync();

            return true;
        }
         public async Task<User> checkUser(string email, string password){
            return await _DBContext.Users.Where(n => n.email.Contains(email)).Where(n => n.password.Contains(password)).FirstOrDefaultAsync();
             }
            
    }
}