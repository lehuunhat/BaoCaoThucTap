using System.Collections.Generic;
using System.Threading.Tasks;
using HienTangToc.Data;
using HienTangToc.Helpers;
using HienTangToc.Interface;
using HienTangToc.Models;
using Microsoft.EntityFrameworkCore;

namespace HienTangToc.Services
{
    public class UserService : UserInterface
    {
        private readonly HientocDbcontext _context;

        public UserService(HientocDbcontext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<(UserModel user, string message)> CreateUserAsync(UserModel user)
        {
            var existingUserByUsername = await _context.Users
           .FirstOrDefaultAsync(u => u.Name == user.Name);

            if (existingUserByUsername != null)
            {
                return (null, "Username đã tồn tại.");
            }

            var existingUserByEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUserByEmail != null)
            {
                return (null, "Email đã tồn tại.");
            }
            user.Password = PasswordHelper.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (user, "Tạo user thành công.");
        }

        public async Task<(UserModel originalUser, UserModel updatedUser, string message)> UpdateUserAsync(int id, UserModel userUpdate)
        {
            if (string.IsNullOrWhiteSpace(userUpdate.Name) ||
                string.IsNullOrWhiteSpace(userUpdate.Email) ||
                string.IsNullOrWhiteSpace(userUpdate.Phone) ||
                string.IsNullOrWhiteSpace(userUpdate.Password) ||
                string.IsNullOrWhiteSpace(userUpdate.Role))
            {
                return (null, null, "Các trường thông tin không được để trống.");
            }

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return (null, null, "Người dùng không tồn tại.");
            }

            var userWithSameEmail = await _context.Users
                .AnyAsync(u => u.Email == userUpdate.Email && u.Id != id);

            if (userWithSameEmail)
            {
                return (null, null, "Email đã tồn tại.");
            }

            var originalUser = new UserModel
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                Email = existingUser.Email,
                Phone = existingUser.Phone,
                Password = existingUser.Password,
                Role = existingUser.Role
            };

            existingUser.Name = userUpdate.Name;
            existingUser.Email = userUpdate.Email;
            existingUser.Phone = userUpdate.Phone;
            existingUser.Password = PasswordHelper.HashPassword(userUpdate.Password);
            existingUser.Role = userUpdate.Role;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return (originalUser, existingUser, "Cập nhật người dùng thành công.");
        }


        public async Task<(bool success, string message)> DeleteUserAsync(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return (false, "Không tìm thấy người dùng.");
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();

            return (true, "Xóa người dùng thành công.");
        }

        public async Task<UserModel> GetUserByEmailOrPhoneAsync(string emailOrPhone)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == emailOrPhone || u.Phone == emailOrPhone);
        }

        public async Task<UserModel> AuthenticateUser(string emailOrPhone, string password)
        {
            var user = await GetUserByEmailOrPhoneAsync(emailOrPhone);

            if (user == null)
            {
                return null;
            }

            if (!PasswordHelper.VerifyPassword(password, user.Password))
            {
                return null;
            }

            return user;
        }

        public async Task<bool> RegisterUserAsync(UserModel model)
        {
            // Kiểm tra xem email hoặc số điện thoại đã tồn tại chưa
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email || u.Phone == model.Phone);

            if (existingUser != null)
            {
                return false; // Tài khoản đã tồn tại
            }

            // Mã hóa mật khẩu và lưu người dùng mới
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new UserModel
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Password = hashedPassword,
                Role = model.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<(UserModel, string)> Register(UserModel model)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (existingUser != null)
            {
                return (null, "Email đã tồn tại.");
            }

            // Mã hóa mật khẩu trước khi lưu
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Lưu người dùng vào cơ sở dữ liệu
            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            return (model, null);
        }
    }
}
