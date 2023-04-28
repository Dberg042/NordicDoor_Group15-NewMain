using Microsoft.CodeAnalysis;
using NordicDoor_Group15.Areas.Identity.Data;
using System.Numerics;

namespace NordicDoor_Group15.Models
{
    public class Membership
    {
        //Many to many relationship between User and Teams
        public int TeamID { get; set; }
        public Team Team { get; set; }

        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
    }
}
