using ClaimsAndRoles.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("AuthDemo"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();


builder.Services.AddAntiforgery();
builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

app.MapGet("/", () => "I am root!");

app.MapGet("/admin-only", () => "Admin only!")
    .RequireAuthorization();

app.MapGet("/account/login", () => "this is the login route");

app.MapGet("/user-claim-check", () => "Access granted to IT department");

var roles = new[] { "Admin", "User" };

app.Run();
