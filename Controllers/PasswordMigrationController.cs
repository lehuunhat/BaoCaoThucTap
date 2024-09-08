using System.Linq;
using System.Threading.Tasks;
using HienTangToc.Data;
using HienTangToc.Helpers;
using HienTangToc.Models;
using Microsoft.AspNetCore.Mvc;

public class PasswordMigrationController : Controller
{
    private readonly HientocDbcontext _context;

    public PasswordMigrationController(HientocDbcontext context)
    {
        _context = context;
    }

    public async Task<IActionResult> MigratePasswords()
    {
        var users = _context.Users.ToList();

        foreach (var user in users)
        {
            // Giả sử mật khẩu chưa được mã hóa và lưu trữ trong thuộc tính Password
            if (!string.IsNullOrEmpty(user.Password))
            {
                string hashedPassword = PasswordHelper.HashPassword(user.Password);
                user.Password = hashedPassword;
                _context.Update(user);
            }
        }

        await _context.SaveChangesAsync();
        return Content("Passwords have been successfully migrated.");
    }
}
