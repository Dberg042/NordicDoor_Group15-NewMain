
using NordicDoor_Group15.Areas.Identity.Data;
using NordicDoor_Group15.Core.Repositories;
using NordicDoor_Group15.Models;

namespace NordicDoor_Group15.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationIdentityDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserRepository(ApplicationIdentityDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            var userName = _contextAccessor.HttpContext.User.Identity.Name;
            return _context.Users.ToList();
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        {
            _context.Update(user);
            _context.SaveChanges();

            return user;
        }

    }
}
