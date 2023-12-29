using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pronia.Domain.Entities;
using Pronia.Domain.Enums;
using Pronia.Persistence.Contexts;

namespace Pronia.Persistence.ContextInitializer;

public class ContextInitializer
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public ContextInitializer(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, AppDbContext context, IConfiguration configuration)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _context = context;
        _configuration = configuration;
    }
    public async Task InitContext()
    {
        await CreateRoles();
        await CreateAdmin();
        UpdateDatabase();
    }
    private async Task CreateRoles()
    {
        foreach (UserRoles role in Enum.GetValues(typeof(UserRoles)))
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new() { Name = role.ToString() });
            }
        }
    }
    private async Task CreateAdmin()
    {
        AppUser user = new()
        {
            UserName = _configuration["AdminSettings:UserName"],
            Fullname = _configuration["AdminSettings:FullName"],
            Email = _configuration["AdminSettings:Email"],
        };

        await _userManager.CreateAsync(user, _configuration["AdminSettings:Password"]);
        await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
    }
    private void UpdateDatabase()
    {
        _context.Database.Migrate();
    }
    
}
