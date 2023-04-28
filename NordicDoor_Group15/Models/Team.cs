using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NordicDoor_Group15.Models
{
    public class Team : Auditable
    {

        public int ID { get; set; }

        [Display(Name = "Team")]
        public string FullName
        {
            get
            {
                return "Team " + TeamNumber + " " + TeamName;
            }
        }

        [Display(Name = "Team Number")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string TeamNumber { get; set; }


        [Display(Name = "Team Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(100, ErrorMessage = "Last name cannot be more than 100 characters long.")]
        public string TeamName { get; set; }

        [Display(Name = "Members")]
        public ICollection<Membership> Memberships { get; set; } = new HashSet<Membership>();

        public ICollection<Suggestion> Suggestions { get; set; } = new HashSet<Suggestion>();   

       
    }
}
