using Microsoft.AspNetCore.Identity;

namespace NordicDoor_Group15.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
