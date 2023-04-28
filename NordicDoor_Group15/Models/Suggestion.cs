using NordicDoor_Group15.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace NordicDoor_Group15.Models
{
    //Suggestion Module
    public class Suggestion : Auditable
    {
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z0-9]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Title should begin with uppercase and should not contain special character")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Title should contain minimum 3 characters.")]
        [Required]
        public string Title { get; set; }
        
        [StringLength(180, MinimumLength = 3, ErrorMessage = "Title should contain minimum 3 characters.")]
        [Required]
        public string Description { get; set; }


        [Required]
        public string MainBody { get; set; }


        [Display(Name = "Team")]
        public int? TeamID { get; set; }
                
        public Team Team { get; set; }
        
        
        [Display(Name = "Creator")]
        public string CreatorID { get; set; }

        public ApplicationUser Creator { get; set; }

        [Display(Name = "Created")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public String Status { get; set; }
        [Required]
        public string Category { get; set; }


        public ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();

        public SuggestionPhoto SuggestionPhoto { get; set; }

        public SuggestionThumbnail SuggestionThumbnail { get; set; }

    }

}
