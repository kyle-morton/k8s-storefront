using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopSphere.App.Data;

namespace ShopSphere.App.Repos;

public abstract class RepoBase
{
    protected ShopSphereDbContext _context;

    public RepoBase(ShopSphereDbContext context)
    {
        _context = context;
    }
}
