using System.Collections.Generic;
using System.Threading.Tasks;
using HienTangToc.Models;
using Microsoft.AspNetCore.Mvc;

namespace HienTangToc.Interface
{
    public interface UserInterface
    {
        Task<List<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10);
        Task<UserModel> GetUserByIdAsync(int id);
        Task<(UserModel user, string message)> CreateUserAsync(UserModel user);
        Task<(UserModel originalUser, UserModel updatedUser, string message)> UpdateUserAsync(int id, UserModel userUpdate);
        Task<(bool success, string message)> DeleteUserAsync(int id);

        Task<UserModel> AuthenticateUser(string emailOrPhone, string password);
        Task<UserModel> GetUserByEmailOrPhoneAsync(string emailOrPhone);
        Task<bool> RegisterUserAsync(UserModel model);
        Task<(UserModel, string)> Register(UserModel model);

    }
}
