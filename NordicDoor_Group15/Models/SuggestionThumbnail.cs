using System.ComponentModel.DataAnnotations;

namespace NordicDoor_Group15.Models
{
    /// <summary>
    /// SuggestionThumbnail in suggestion index page.  
    /// </summary>
    public class SuggestionThumbnail
    {
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Content { get; set; }

        [StringLength(255)]
        public string MimeType { get; set; }

        public int SuggestionID { get; set; }
        public Suggestion Suggestion { get; set; }
    }
}
