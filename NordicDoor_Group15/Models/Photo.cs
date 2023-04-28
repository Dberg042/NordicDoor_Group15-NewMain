using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NordicDoor_Group15.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        //[Required(ErrorMessage = "title your photo")]
        
        public string Title { get; set; }

        [NotMapped]
        [DisplayName("Upload Photo")]
        public IFormFile PhotoInIForm { get; set; }
        
        public int SuggestionID { get; set; }
        public Suggestion Suggestion { get; set; }
        

        public string PhotoName { get; set; }

        //Activate these properties for saving photo to database
        //[NotMapped]
        //public string PhotoPath { get; set; }

        //[NotMapped]
        //[DisplayName("Size in MB")]
        //public decimal DisplaySize { get; set; }

        //[NotMapped]
        //public byte[] PhotoInBytes { get; set; }

    }
}
