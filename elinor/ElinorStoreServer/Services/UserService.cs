using ElinorStoreServer.Data.Domain;
using ElinorStoreServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using share.Models.User;

namespace ElinorStoreServer.Services
{
    public class UserService
    {
        private readonly StoreDbContext _context;
        public UserService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<AppUser?> GetAsync(string id)
        {
            AppUser? user = await _context.Users.FindAsync(id);
            return user;
        }
        public async Task<List<AppUser>> GetsAsync()
        {
            List<AppUser> users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task AddAsync(UserAddRequestDto model)
        {
            AppUser user = new AppUser
            {
                Name = model.Name,
                LastName = model.LastName,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task EditAsync(AppUser user)
        {
            AppUser? oldUser = await _context.Users.FindAsync(user.Id);
            if (oldUser is null)
            {
                throw new Exception("کاربری با این شناسه پیدا نشد.");
            }
            oldUser.Name = user.Name;
            oldUser.LastName = user.LastName;
            _context.Users.Update(oldUser);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(string id)
        {
            AppUser? user = await _context.Users.FindAsync(id);
            if (user is null)
            {
                throw new Exception("کاربری با این شناسه پیدا نشد.");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        internal async Task EditAsync(object user)
        {
            throw new NotImplementedException();
        }
    }
}