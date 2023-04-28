namespace NordicDoor_Group15.Models
{
    //Interface for automating who and when created and edited entry
    internal interface IAuditable
    {
        string CreatedBy { get; set; }
        DateTime? CreatedOn { get; set; }
        string UpdatedBy { get; set; }
        DateTime? UpdatedOn { get; set; }
    }
}
