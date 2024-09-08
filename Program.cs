using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using HienTangToc.Data;
using HienTangToc.Interface;
using HienTangToc.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext với chuỗi kết nối
builder.Services.AddDbContext<HientocDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));

// Đăng ký các dịch vụ cho DI container
builder.Services.AddScoped<UserInterface, UserService>();
builder.Services.AddScoped<SalonInterface, SalonService>();
builder.Services.AddScoped<NguoihienInterface, NguoihienService>();
builder.Services.AddScoped<NguoimuonInterface, NguoimuonService>();
builder.Services.AddScoped<StatisticsService>();
builder.Services.AddScoped<NguoihienService>();
builder.Services.AddScoped<NguoimuonService>();
builder.Services.AddScoped<SalonService>();

// Cấu hình session
builder.Services.AddDistributedMemoryCache(); // Dịch vụ cache phân phối
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Cấu hình Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.LogoutPath = "/Logout/Logout";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

// Cấu hình Razor view location formats
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Views/Account/{0}.cshtml");
        options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
    });

var app = builder.Build();

// Cấu hình pipeline HTTP request
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Thêm middleware xác thực
app.UseAuthorization();

app.UseSession(); // Thêm middleware session

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
