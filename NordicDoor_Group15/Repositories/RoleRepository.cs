using NordicDoor_Group15.Core.Repositories;
using NordicDoor_Group15.Areas.Identity.Data;
using NordicDoor_Group15.Data;
using Microsoft.AspNetCore.Identity;

namespace NordicDoor_Group15.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationIdentityDbContext _context;

        public RoleRepository(ApplicationIdentityDbContext context)
        {
            _context = context;
        }
        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
