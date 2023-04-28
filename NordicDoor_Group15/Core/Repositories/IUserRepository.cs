using NordicDoor_Group15.Areas.Identity.Data;
using NordicDoor_Group15.Models;

namespace NordicDoor_Group15.Core.Repositories
{
    public interface IUserRepository 
    {
        ICollection<ApplicationUser> GetUsers();

        ApplicationUser GetUser(string id);

        ApplicationUser UpdateUser(ApplicationUser user);

      
    }
}
