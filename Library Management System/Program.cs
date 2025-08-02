using library.DataModel;
using library.DataModel.Models;
using Library.Manager.Interface;
using Library.Manager.Implement;
using Library_Management_System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Library.Service.Implement;
using Library.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<FineUpdateService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<LibraryDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailSender,EmailSender>();
builder.Services.AddScoped<IBookManager, BookManager>();
builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddScoped<IStudentManager, StudentManager>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IBookRequestManager, BookRequestManager>();
builder.Services.AddScoped<IBookRequestService, BookRequestService>();
builder.Services.AddScoped<IIssuedBookManager, IssuedBookManager>();
builder.Services.AddScoped<IIssuedBookService, IssuedBookService>();
builder.Services.AddScoped<IFineManager, FineManager>();
builder.Services.AddScoped<IFineService, FineService>();
builder.Services.AddScoped<IDashboardManager, DashboardManager>();
builder.Services.AddScoped<IDashboardService, DashboardService>();


builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});


var app = builder.Build();

// Seed roles and admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedRolesAndAdminAsync(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
