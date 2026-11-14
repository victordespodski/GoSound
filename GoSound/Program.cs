using GoSound.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connection))
    throw new InvalidOperationException("Set ConnectionStrings:DefaultConnection with dotnet user-secrets. See README.md.");
builder.Services.AddDbContext<GoSoundDbContext>(options => options.UseSqlServer(connection));
var app = builder.Build();
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Error/ServerError"); app.UseHsts(); }
app.UseStatusCodePagesWithReExecute("/Error/Status/{0}");
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.Run();
