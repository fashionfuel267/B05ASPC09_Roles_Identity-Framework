using B05ASPC09_Secutity01.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(
//    options => options.SignIn.RequireConfirmedAccount = true)
   
//    ;
builder.Services.AddIdentity<IdentityUser, IdentityRole>(op =>
{
    op.Password.RequiredLength = 7;
    op.Password.RequireUppercase = false;
    op.User.RequireUniqueEmail = true;
    op.Password.RequireDigit = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
