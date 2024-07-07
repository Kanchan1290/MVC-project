using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PracticeProjectContext>(o => o.UseSqlServer(builder.Configuration["Conn"]));
builder.Services.AddSingleton<DataSecurityProvider>();

// Add this line to register data protection services
//builder.Services.AddDataProtection();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Home}/{Action=Index}/{id?}"
);

app.Run();
