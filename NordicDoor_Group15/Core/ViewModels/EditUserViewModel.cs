using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NordicDoor_Group15.Areas.Identity.Data;

namespace NordicDoor_Group15.Core.ViewModels
{
    public class EditUserViewModel
    {
        public ApplicationUser User { get; set; }

        public IList<SelectListItem> Roles { get; set; }

        public string StatusMessage { get; set; }
        public List<string> UserRoles { get; internal set; }

        
    }
}
