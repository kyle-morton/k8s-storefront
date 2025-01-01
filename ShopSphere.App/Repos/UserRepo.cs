using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopSphere.App.Data;
using ShopSphere.App.Domain;

namespace ShopSphere.App.Repos;

public interface IUserRepo {
    Task<User> Get(int id);
    Task<List<User>> Get();
}

public class UserRepo : RepoBase, IUserRepo
{
    public UserRepo(ShopSphereDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> Get(int id)
    {
        return await _context.Users
        .Include(u => u.Orders)
        .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<User>> Get()
    {
        return await _context.Users
        .Include(u => u.Orders)
        .ToListAsync();
    }
}
